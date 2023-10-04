using System.Data;
using BackServer.Contexts;
using BackServer.Repositories;
using BackServer.RepositoryChangers.Interfaces;
using BackServer.RepositoryVisitors;
using DbEntity;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using NpgsqlDbExtensions;
using NpgsqlDbExtensions.Enums;
using NpgsqlTypes;
using Property = Entity.Property;

namespace BackServer.RepositoryChangers.Implementations;

public class FilterChangerDb : IFilterChanger
{
    private readonly GsDbContext _context;
    private readonly IPropertyChanger _propertyChanger;
    private readonly IPropertyVisitor _propertyVisitor;
    private readonly IProductVisitor _productVisitor;
    private readonly DbEntityGetter _dbEntityGetter;
    private readonly string defaultPropertyValue = "Не указано";

    public FilterChangerDb(GsDbContext context, IPropertyChanger propertyChanger, IPropertyVisitor propertyVisitor, IProductVisitor productVisitor, DbEntityGetter dbEntityGetter)
    {
        _context = context;
        _propertyChanger = propertyChanger;
        _propertyVisitor = propertyVisitor;
        _productVisitor = productVisitor;
        _dbEntityGetter = dbEntityGetter;
    }

    public async Task<bool> AddFilterHeadingOne(string headingTitle, string propertyTitle)
    {
        return await AddFilterHeading(headingTitle, propertyTitle, Headings.HeadingOne);
    }
    
    public async Task<bool> AddFilterHeadingTwo(string headingTwoTitle, string propertyTitle)
    {
        return await AddFilterHeading(headingTwoTitle, propertyTitle, Headings.HeadingTwo);
    }

    public async Task<bool> AddFilterHeading(string headingTitle, string propertyTitle, Headings headingType)
    {
        var property = await _dbEntityGetter.TryGetProperty(propertyTitle);
        if (property == null) return false;
        
        IHeading? heading;
        IEnumerable<Entity.Product> products;
        switch (headingType)
        {
            case Headings.HeadingOne:
                heading = await _dbEntityGetter.TryGetHeadingOne(headingTitle);
                products = await _productVisitor.GetAllHeadingOne(headingTitle);
                break;
            case Headings.HeadingTwo:
                heading = await _dbEntityGetter.TryGetHeadingTwo(headingTitle);
                products = await _productVisitor.GetAllHeadingTwo(headingTitle);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(headingType), headingType, null);
        }

        
        foreach (var product in products)
        {
            var propertyValue = await _propertyVisitor.GetProductPropertyValue(product.Title, propertyTitle);
            DbEntity.PropertyValues propertyValueDb;
            if (propertyValue == null)
            {
                await _propertyChanger.AddProductPropertyValue(product.Title, propertyTitle, defaultPropertyValue,
                    false);
                propertyValueDb =
                    await _dbEntityGetter.TryGetPropertyValueDb(propertyTitle, defaultPropertyValue);
            }
            else
            {
                propertyValueDb =
                    await _dbEntityGetter.TryGetPropertyValueDb(propertyTitle, propertyValue);
            }

            switch (headingType)
            {
                case Headings.HeadingOne:
                    await AddFilterHeadingOne((DbEntity.HeadingOne) heading, propertyValueDb);
                    break;
                case Headings.HeadingTwo:
                    await AddFilterHeadingTwo((DbEntity.HeadingTwo) heading, propertyValueDb);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(headingType), headingType, "Неверно указан тип звголовка при добавлении филтьра");
            }
            
        }
        
        return true;
    }

    private async Task AddFilterHeadingOne(DbEntity.HeadingOne headingOne, DbEntity.PropertyValues propertyValue)
    {
        var headingFilter = await _dbEntityGetter.TryGetHeadingOneFilter(headingOne.Id, propertyValue.Id);
        if (headingFilter != null)
        {
            await AddCountHeadingOneFilter(headingOne.Title, propertyValue.PropertyValue, 1);
            return;
        }
        
        var dbConnection = (NpgsqlConnection?) _context.Database.GetDbConnection();
        if (dbConnection.State != ConnectionState.Open)
            await dbConnection.OpenAsync();
        
        var sql = @"INSERT INTO heading_one_filters (heading_one_id, property_values_id, count_products)
                VALUES (@HID, @PVID, 1);";

        await NpgsqlFunctions.Execute(sql, dbConnection, new[]
        {
            new NpgsqlParameter()
                {ParameterName = "@HID", NpgsqlDbType = NpgsqlDbType.Integer, Value = headingOne.Id},
            new NpgsqlParameter()
                {ParameterName = "@PVID", NpgsqlDbType = NpgsqlDbType.Integer, Value = propertyValue.Id}
        });

        await dbConnection.CloseAsync();
    }

    private async Task AddFilterHeadingTwo(DbEntity.HeadingTwo headingTwo, DbEntity.PropertyValues propertyValue)
    {
        var headingFilter = await _dbEntityGetter.TryGetHeadingTwoFilter(headingTwo.Id, propertyValue.Id);
        if (headingFilter != null)
        {
            await AddCountHeadingTwoFilter(headingTwo.Title, propertyValue.PropertyValue, 1);
            return;
        }

        var dbConnection = (NpgsqlConnection?) _context.Database.GetDbConnection();
        if (dbConnection.State != ConnectionState.Open)
            await dbConnection.OpenAsync();
        
        var sql = @"INSERT INTO heading_two_filters (heading_two_id, property_values_id, count_products)
                VALUES (@HID, @PVID, 1);";

        await NpgsqlFunctions.Execute(sql, dbConnection, new[]
        {
            new NpgsqlParameter()
                {ParameterName = "@HID", NpgsqlDbType = NpgsqlDbType.Integer, Value = headingTwo.Id},
            new NpgsqlParameter()
                {ParameterName = "@PVID", NpgsqlDbType = NpgsqlDbType.Integer, Value = propertyValue.Id}
        });
        
        await dbConnection.CloseAsync();
    }

    public async Task<bool> DeleteHeadingOneFilter(string propertyTitle, string headingTitle)
    {
        return await DeleteHeadingFilter(propertyTitle, headingTitle, Headings.HeadingOne);
    }

    public async Task<bool> DeleteHeadingTwoFilter(string propertyTitle, string headingTitle)
    {
        return await DeleteHeadingFilter(propertyTitle, headingTitle, Headings.HeadingTwo);
    }

    public async Task<bool> DeleteHeadingFilter(string propertyTitle, string headingTitle, Headings headingType)
    {
        var dbConnection = (NpgsqlConnection?) _context.Database.GetDbConnection();
        if (dbConnection.State != ConnectionState.Open)
            await dbConnection.OpenAsync();

        var headingId = await GetIdByHeadingType(headingTitle, headingType);
        if (headingId < 0)
            return true;


        var products = await _productVisitor.GetAllHeadingOne(headingTitle);
        foreach (var product in products)
        {
            var propertyValue = product.Properties.Where(x => x.Title == propertyTitle).Select(x => x.Values).First()
                .First();
            var propertyValueDb =
                await _dbEntityGetter.TryGetPropertyValueDb(propertyTitle, propertyValue);

            await DeleteHeadingFilter(headingId, propertyValueDb.Id, headingType, dbConnection);
        }

        await dbConnection.CloseAsync();
        return true;
    }

    private async Task DeleteHeadingFilter(int headingId, int propertyValueId, Headings heading,
        NpgsqlConnection? dbConnection)
    {
        var sql = heading switch
        {
            Headings.HeadingOne =>
                @"DELETE FROM heading_one_filters WHERE heading_one_id = @HID AND property_values_id = @PVID;",
            Headings.HeadingTwo =>
                @"DELETE FROM heading_two_filters WHERE heading_two_id = @HID AND property_values_id = @PVID;",
            _ => throw new ArgumentException("Неверно указан тип заголовка при удалении")
        };

        await NpgsqlFunctions.Execute(sql, dbConnection, new[]
        {
            new NpgsqlParameter()
                {ParameterName = "@HID", NpgsqlDbType = NpgsqlDbType.Integer, Value = headingId},
            new NpgsqlParameter()
                {ParameterName = "@PVID", NpgsqlDbType = NpgsqlDbType.Integer, Value = propertyValueId}
        });
    }

    public async Task<bool> DeleteAllHeadingOneFilters(string headingOneTitle)
    {
        return await DeleteAllHeadingFilters(headingOneTitle, Headings.HeadingOne);
    }

    public async Task<bool> DeleteAllHeadingTwoFilters(string headingTwoTitle)
    {
        return await DeleteAllHeadingFilters(headingTwoTitle, Headings.HeadingTwo);
    }

    public async Task<bool> DeleteAllHeadingFilters(string headingTitle, Headings headingType)
    {
        var dbConnection = (NpgsqlConnection?) _context.Database.GetDbConnection();
        if (dbConnection.State != ConnectionState.Open)
            await dbConnection.OpenAsync();

        var headingId = await GetIdByHeadingType(headingTitle, headingType);
        if (headingId < 0)
            return true;

        await DeleteAllHeadingFilters(headingId, headingType, dbConnection);

        await dbConnection.CloseAsync();
        return true;
    }

    private async Task DeleteAllHeadingFilters(int headingId, Headings heading, NpgsqlConnection dbConnection)
    {
        string sql = heading switch
        {
            Headings.HeadingOne => @"DELETE FROM heading_one_filters WHERE heading_one_id = @HID",
            Headings.HeadingTwo => @"DELETE FROM heading_two_filters WHERE heading_two_id = @HID",
            _ => throw new ArgumentException("Неверно указан тип заголовка при удалении")
        };

        await NpgsqlFunctions.Execute(sql, dbConnection, new[]
        {
            new NpgsqlParameter()
                {ParameterName = "@HID", NpgsqlDbType = NpgsqlDbType.Integer, Value = headingId}
        });
    }

    private async Task<int> GetIdByHeadingType(string headingTitle, Headings headingType)
    {
        switch (headingType)
        {
            case Headings.HeadingOne:
                var headingOne = await _dbEntityGetter.TryGetHeadingOne(headingTitle);
                if (headingOne == null) return -1;
                return headingOne.Id;

            case Headings.HeadingTwo:
                var headingTwo = await _dbEntityGetter.TryGetHeadingTwo(headingTitle);
                if (headingTwo == null) return -1;
                return headingTwo.Id;
            default:
                throw new ArgumentException("Неверно указан тип заголовка при удалении");
        }
    }


    public async Task AddCountHeadingOneFilter(string headingTitle, string propertyValue, int count)
    {
        await AddCountHeadingFilter(headingTitle, Headings.HeadingOne, propertyValue, count);
    }
    
    public async Task AddCountHeadingTwoFilter(string headingTwoTitle, string propertyValue, int count)
    {
        await AddCountHeadingFilter(headingTwoTitle, Headings.HeadingTwo, propertyValue, count);
    }
    
    private async Task AddCountHeadingFilter(string headingTitle,  Headings headingType, string propertyValue, int count)
    {
        string sql;
        IHeadingFilter? headingFilter;
        switch (headingType)
        {
            case Headings.HeadingOne:
                headingFilter =
                    await _dbEntityGetter.TryGetHeadingOneFilter(headingTitle, propertyValue);
                if (headingFilter == null) return;
                sql =
                    @"UPDATE heading_one_filters SET count_products=@COUNT WHERE heading_one_id=@HID AND property_values_id=@PVID;";
                break;
            case Headings.HeadingTwo:
                headingFilter =
                    await _dbEntityGetter.TryGetHeadingTwoFilter(headingTitle, propertyValue);
                if (headingFilter == null) return;
                sql =
                    @"UPDATE heading_two_filters SET count_products=@COUNT WHERE heading_two_id=@HID AND property_values_id=@PVID;";
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(headingType), headingType, null);
        }
        
        var dbConnection = (NpgsqlConnection?) _context.Database.GetDbConnection();
        if (dbConnection.State != ConnectionState.Open)
            await dbConnection.OpenAsync();
        
        await NpgsqlFunctions.Execute(sql, dbConnection, new[]
        {
            new NpgsqlParameter()
            {
                ParameterName = "@COUNT", NpgsqlDbType = NpgsqlDbType.Integer, Value = headingFilter.Count + count
            },
            new NpgsqlParameter()
                {ParameterName = "@HID", NpgsqlDbType = NpgsqlDbType.Integer, Value = headingFilter.heading_id},
            new NpgsqlParameter()
            {
                ParameterName = "@PVID", NpgsqlDbType = NpgsqlDbType.Integer,
                Value = headingFilter.property_values_id
            }
        });
        
        await dbConnection.CloseAsync();
    }
}