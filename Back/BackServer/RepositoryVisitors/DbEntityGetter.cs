using System.Data;
using BackServer.Contexts;
using DbEntity;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using NpgsqlDbExtensions;
using NpgsqlTypes;

namespace BackServer.RepositoryVisitors;

public class DbEntityGetter
{
    private readonly GsDbContext _context;

    public DbEntityGetter(GsDbContext context)
    {
        _context = context;
    }

    public async Task<DbEntity.PropertyValues?> TryGetPropertyValueDb(string propertyTitle, string propertyValue)
    {
        var dbConnection = (NpgsqlConnection?) _context.Database.GetDbConnection();
        if (dbConnection.State != ConnectionState.Open)
            await dbConnection.OpenAsync();
        
        var sql = @"SELECT property_values_id, property_value
                    FROM property_values 
                    JOIN properties p on p.property_id = property_values.property_id
                    WHERE property_value = @VALUE AND p.title = @TITLE;";

        var properties = await NpgsqlFunctions.TryGet(sql, dbConnection, new[]
            {
                new NpgsqlParameter()
                    {ParameterName = "@VALUE", NpgsqlDbType = NpgsqlDbType.Text, Value = propertyValue},
                new NpgsqlParameter()
                    {ParameterName = "@TITLE", NpgsqlDbType = NpgsqlDbType.Text, Value = propertyTitle}
            },
            reader => new DbEntity.PropertyValues() {Id = reader.GetInt32(0), PropertyValue = reader.GetString(1)});

        await dbConnection.CloseAsync();
        return properties.Count == 0 ? null : properties.First();
    }

    public async Task<DbEntity.Property?> TryGetProperty(string propertyTitle)
    {
        var dbConnection = (NpgsqlConnection?) _context.Database.GetDbConnection();
        if (dbConnection.State != ConnectionState.Open)
            await dbConnection.OpenAsync();
        
        
        var sql = @"SELECT property_id, title
                    FROM properties
                    WHERE title = @TITLE;";

        var properties = await NpgsqlFunctions.TryGet(sql, dbConnection, new[]
            {
                new NpgsqlParameter()
                    {ParameterName = "@TITLE", NpgsqlDbType = NpgsqlDbType.Text, Value = propertyTitle}
            },
            reader => new DbEntity.Property() {Id = reader.GetInt32(0), Title = reader.GetString(1)});

        await dbConnection.CloseAsync();
        return properties.Count == 0 ? null : properties.First();
    }

    public async Task<DbEntity.HeadingOne?> TryGetHeadingOne(string headingOneTitle)
    {
        var dbConnection = (NpgsqlConnection?) _context.Database.GetDbConnection();
        if (dbConnection.State != ConnectionState.Open)
            await dbConnection.OpenAsync();
        
        var sql = @"SELECT heading_one_id, title
                FROM heading_one
                WHERE title = @TITLE;";

        var headingsOne = await NpgsqlFunctions.TryGet(sql, dbConnection, new[]
            {
                new NpgsqlParameter()
                    {ParameterName = "@TITLE", NpgsqlDbType = NpgsqlDbType.Varchar, Value = headingOneTitle}
            },
            reader => new DbEntity.HeadingOne() {Id = reader.GetInt32(0), Title = reader.GetString(1)});

        await dbConnection.CloseAsync();
        return headingsOne.Count == 0 ? null : headingsOne.First();
    }

    public async Task<DbEntity.HeadingTwo?> TryGetHeadingTwo(string headingTwoTitle)
    {
        var dbConnection = (NpgsqlConnection?) _context.Database.GetDbConnection();
        if (dbConnection.State != ConnectionState.Open)
            await dbConnection.OpenAsync();
        
        var sql = @"SELECT heading_two_id, title
                FROM heading_two
                WHERE title = @TITLE;";

        var headingsTwo = await NpgsqlFunctions.TryGet(sql, dbConnection, new[]
            {
                new NpgsqlParameter()
                    {ParameterName = "@TITLE", NpgsqlDbType = NpgsqlDbType.Text, Value = headingTwoTitle}
            },
            reader => new DbEntity.HeadingTwo() {Id = reader.GetInt32(0), Title = reader.GetString(1)});

        await dbConnection.CloseAsync();
        return headingsTwo.Count == 0 ? null : headingsTwo.First();
    }

    public async Task<DbEntity.HeadingOneFilters?> TryGetHeadingOneFilter(int headingOneId, int propertyValueId)
    {
        var dbConnection = (NpgsqlConnection?) _context.Database.GetDbConnection();
        if (dbConnection.State != ConnectionState.Open)
            await dbConnection.OpenAsync();
        
        var sql = @"SELECT heading_one_id, property_values_id, count_products
                FROM heading_one_filters
                WHERE heading_one_id = @HEADINGID AND property_values_id = @PVID;";

        var headingOneFilters = await NpgsqlFunctions.TryGet(sql, dbConnection, new[]
            {
                new NpgsqlParameter()
                    {ParameterName = "@HEADINGID", NpgsqlDbType = NpgsqlDbType.Integer, Value = headingOneId},
                new NpgsqlParameter()
                    {ParameterName = "@PVID", NpgsqlDbType = NpgsqlDbType.Integer, Value = propertyValueId}
            },
            reader => new HeadingOneFilters()
            {
                heading_id = reader.GetInt32(0), property_values_id = reader.GetInt32(1), Count = reader.GetInt32(2)
            });

        await dbConnection.CloseAsync();
        return headingOneFilters.Count == 0 ? null : headingOneFilters.First();
    }

    public async Task<DbEntity.HeadingOneFilters?> TryGetHeadingOneFilter(string headingOneTitle, string propertyValue)
    {
        var dbConnection = (NpgsqlConnection?) _context.Database.GetDbConnection();
        if (dbConnection.State != ConnectionState.Open)
            await dbConnection.OpenAsync();
        
        var sql = @"SELECT hof.heading_one_id, hof.property_values_id, count_products
                FROM heading_one_filters hof
                JOIN property_values pv on pv.property_values_id = hof.property_values_id
                JOIN heading_one ho on ho.heading_one_id = hof.heading_one_id
                WHERE ho.title = @HTITLE AND property_value = @VALUE;";

        var headingOneFilters = await NpgsqlFunctions.TryGet(sql, dbConnection, new[]
            {
                new NpgsqlParameter()
                    {ParameterName = "@HTITLE", NpgsqlDbType = NpgsqlDbType.Text, Value = headingOneTitle},
                new NpgsqlParameter()
                    {ParameterName = "@VALUE", NpgsqlDbType = NpgsqlDbType.Text, Value = propertyValue}
            },
            reader => new HeadingOneFilters()
            {
                heading_id = reader.GetInt32(0), property_values_id = reader.GetInt32(1), Count = reader.GetInt32(2)
            });

        await dbConnection.CloseAsync();
        return headingOneFilters.Count == 0 ? null : headingOneFilters.First();
    }

    public async Task<DbEntity.HeadingTwoFilters?> TryGetHeadingTwoFilter(int headingTwoId, int propertyValueId)
    {
        var dbConnection = (NpgsqlConnection?) _context.Database.GetDbConnection();
        if (dbConnection.State != ConnectionState.Open)
            await dbConnection.OpenAsync();
        
        var sql = @"SELECT heading_two_id, property_values_id, count_products
                FROM heading_two_filters
                WHERE heading_two_id = @HEADINGID AND property_values_id = @PVID;";

        var headingTwoFilters = await NpgsqlFunctions.TryGet(sql, dbConnection, new[]
            {
                new NpgsqlParameter()
                    {ParameterName = "@HEADINGID", NpgsqlDbType = NpgsqlDbType.Integer, Value = headingTwoId},
                new NpgsqlParameter()
                    {ParameterName = "@PVID", NpgsqlDbType = NpgsqlDbType.Integer, Value = propertyValueId}
            },
            reader => new HeadingTwoFilters()
            {
                heading_id = reader.GetInt32(0), property_values_id = reader.GetInt32(1), Count = reader.GetInt32(2)
            });

        await dbConnection.CloseAsync();
        return headingTwoFilters.Count == 0 ? null : headingTwoFilters.First();
    }

    public async Task<DbEntity.HeadingTwoFilters?> TryGetHeadingTwoFilter(string headingTwoTitle,
        string propertyValue)
    {
        var dbConnection = (NpgsqlConnection?) _context.Database.GetDbConnection();
        if (dbConnection.State != ConnectionState.Open)
            await dbConnection.OpenAsync();
        
        var sql = @"SELECT htf.heading_two_id, htf.property_values_id, count_products
                FROM heading_two_filters htf
                JOIN property_values pv on pv.property_values_id = htf.property_values_id
                JOIN heading_two ht on ht.heading_two_id = htf.heading_two_id
                WHERE ht.title = @HTITLE AND property_value = @VALUE;";

        var headingTwoFilters = await NpgsqlFunctions.TryGet(sql, dbConnection, new[]
            {
                new NpgsqlParameter()
                    {ParameterName = "@HTITLE", NpgsqlDbType = NpgsqlDbType.Text, Value = headingTwoTitle},
                new NpgsqlParameter()
                    {ParameterName = "@VALUE", NpgsqlDbType = NpgsqlDbType.Text, Value = propertyValue}
            },
            reader => new HeadingTwoFilters()
            {
                heading_id = reader.GetInt32(0), property_values_id = reader.GetInt32(1), Count = reader.GetInt32(2)
            });

        await dbConnection.CloseAsync();
        return headingTwoFilters.Count == 0 ? null : headingTwoFilters.First();
    }
}