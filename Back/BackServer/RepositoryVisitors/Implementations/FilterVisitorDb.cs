using System.Data;
using System.Text;
using BackServer.Contexts;
using Entity;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using NpgsqlDbExtensions;
using NpgsqlDbExtensions.Enums;
using NpgsqlTypes;

namespace BackServer.Repositories;

public class FilterVisitorDb : IFilterVisitor
{
    private readonly GsDbContext _context;

    public FilterVisitorDb(GsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Entity.Property>> GetAllHeadingOneFilters(string headingOneTitle)
    {
        var res = (await GetMaxMinPrice(headingOneTitle, Headings.HeadingOne)).ToList();
        res.AddRange(await GetHeadingsOneFiltersAsync(headingOneTitle));

        return res;
    }

    public async Task<IEnumerable<Entity.Property>> GetAllHeadingTwoFilters(string headingTwoTitle)
    {
        var res = (await GetMaxMinPrice(headingTwoTitle, Headings.HeadingTwo)).ToList();
        res.AddRange(await GetHeadingsTwoFiltersAsync(headingTwoTitle));
        return res;
    }

    private async Task<IEnumerable<Entity.Property>> GetMaxMinPrice(string headingTitle, Headings heading)
    {
        var res = new List<Entity.Property>();
        var dbConnection = (NpgsqlConnection?) _context.Database.GetDbConnection();

        var sql = new StringBuilder();
        sql.Append(@$"
                SELECT max(CASE WHEN s.percent ISNULL THEN p.price ELSE p.price * (100 - s.percent) / 100 END),
                       min(CASE WHEN s.percent ISNULL THEN p.price ELSE p.price * (100 - s.percent) / 100 END)
                FROM products as p
                         JOIN product_family pf on pf.product_family_id = p.product_family_id
                         LEFT JOIN sale_products sp on p.product_id = sp.product_id
                         LEFT JOIN sales s on sp.sale_id = s.sale_id");

        switch (heading)
        {
            case Headings.HeadingOne:
                sql.Append($@" JOIN heading_one ho on ho.heading_one_id = pf.heading_one_id
                                WHERE ho.title = '{headingTitle}'
                                GROUP BY pf.heading_one_id;");
                break;
            case Headings.HeadingTwo:
                sql.Append($@" JOIN heading_two ht on ht.heading_two_id = pf.heading_two_id
                                WHERE ht.title = '{headingTitle}'
                                GROUP BY pf.heading_two_id;");
                break;
        }

        if (dbConnection.State != ConnectionState.Open)
            await dbConnection.OpenAsync();
        await using var command = new NpgsqlCommand(sql.ToString(), dbConnection);
        await using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            res.Add(new Entity.Property("Максимальная цена", new[] {reader.GetInt32(0).ToString()}));
            res.Add(new Entity.Property("Минимальная цена", new[] {reader.GetInt32(1).ToString()}));
        }

        await dbConnection.CloseAsync();
        return res;
    }

    private async Task<IEnumerable<Property>> GetHeadingsOneFiltersAsync(string headingOneTitle)
    {
        var sql = @"
                    SELECT p.title, pv.property_value
                    FROM heading_one as ho
                    JOIN heading_one_filters hof on ho.heading_one_id = hof.heading_one_id
                    JOIN property_values pv on hof.property_values_id = pv.property_values_id
                    JOIN properties p on p.property_id = pv.property_id
                    WHERE ho.title=@TITLE;";

        return await GetFilters(sql,
            new[]
            {
                new NpgsqlParameter()
                    {ParameterName = "@TITLE", NpgsqlDbType = NpgsqlDbType.Text, Value = headingOneTitle}
            });
    }

    private async Task<IEnumerable<Entity.Property>> GetHeadingsTwoFiltersAsync(string headingTwoTitle)
    {
        var sql = @"
                    SELECT p.title, pv.property_value
                    FROM heading_two as ht
                    JOIN heading_two_filters htf on ht.heading_two_id = htf.heading_two_id
                    JOIN property_values pv on htf.property_values_id = pv.property_values_id
                    JOIN properties p on p.property_id = pv.property_id
                    WHERE ht.title=@TITLE;";

        return await GetFilters(sql,
            new[]
            {
                new NpgsqlParameter()
                    {ParameterName = "@TITLE", NpgsqlDbType = NpgsqlDbType.Text, Value = headingTwoTitle}
            });
    }

    private async Task<IEnumerable<Property>> GetFilters(string sql, IEnumerable<NpgsqlParameter> parameters)
    {
        var properties = new Dictionary<string, Property>();
        var dbConnection = (NpgsqlConnection?) _context.Database.GetDbConnection();

        if (dbConnection.State != ConnectionState.Open)
            await dbConnection.OpenAsync();

        await using var command = new NpgsqlCommand(sql, dbConnection);
        {
            NpgsqlFunctions.AddParameters(command, parameters);
            await using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var title = reader.GetString(0);
                var propertyValue = reader.GetString(1);
                if (!properties.ContainsKey(title))
                    properties.Add(title, new Property(title, new List<string>()));
                properties[title].Values.Add(propertyValue);
            }
        }

        await dbConnection.CloseAsync();
        return properties.Values;
    }
}