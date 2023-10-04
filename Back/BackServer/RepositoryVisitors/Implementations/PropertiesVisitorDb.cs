using System.Data;
using System.Text;
using BackServer.Contexts;
using BackServer.Repositories;
using Entity;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using NpgsqlDbExtensions.Enums;

namespace BackServer.RepositoryVisitors.Implementations
{
    public class PropertiesVisitorDb : IPropertyVisitor
    {
        private readonly GsDbContext _context;

        public PropertiesVisitorDb(GsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<string>> GetAllTitles()
        {
            return await _context.Properties.Select(x => x.Title).ToArrayAsync();
        }

        public async Task<IEnumerable<Entity.Property>> GetAllByProduct(string productTitle)
        {
            var res = new List<Entity.Property>();
            var con = (NpgsqlConnection?) _context.Database.GetDbConnection();

            if (con.State != ConnectionState.Open)
                await con.OpenAsync();

            var sql = @$"
            SELECT prop.title, p1.property_value 
            FROM products AS p
                     JOIN product_properties pp on p.product_id = pp.product_id
                     JOIN property_values AS p1 ON pp.property_values_id = p1.property_values_id
                     JOIN properties as prop ON p1.property_id = prop.property_id
            WHERE p.title = '{productTitle}'
            ORDER BY prop.property_id;";
            await using var cmd = new NpgsqlCommand(sql, con);
            await using NpgsqlDataReader rdr = await cmd.ExecuteReaderAsync();
            while (await rdr.ReadAsync())
            {
                res.Add(new Entity.Property(rdr.GetString(0), new[] {rdr.GetString(1)}));
            }

            await con.CloseAsync();

            return res;
        }

        public async Task<IEnumerable<Entity.Property>> GetPriorityByProduct(string productTitle)
        {
            var properties = new List<Entity.Property>();
            var con = (NpgsqlConnection?) _context.Database.GetDbConnection();
            if (con.State != ConnectionState.Open)
                await con.OpenAsync();

            var sql = @$"
                SELECT p2.title, pv.property_value
                FROM products as p
                         LEFT JOIN (SELECT * FROM product_properties WHERE is_priority) as pp on p.product_id = pp.product_id
                         LEFT JOIN property_values pv on pp.property_values_id = pv.property_values_id
                         LEFt JOIN properties p2 on pv.property_id = p2.property_id
                WHERE p.title= '{productTitle}'
                ORDER BY p2.property_id;";

            await using var cmd = new NpgsqlCommand(sql, con);
            await using NpgsqlDataReader rdr = await cmd.ExecuteReaderAsync();
            while (await rdr.ReadAsync())
            {
                if (!rdr.IsDBNull(0))
                    properties.Add(new Entity.Property(rdr.GetString(0), new[] {rdr.GetString(1)}));
            }

            await con.CloseAsync();

            return properties;
        }

        public async Task<string?> GetProductPropertyValue(string productTitle, string propertyTitle)
        {
            var con = (NpgsqlConnection?) _context.Database.GetDbConnection();

            if (con.State != ConnectionState.Open)
                await con.OpenAsync();

            var sql = @$"
                    SELECT pv.property_value
                    FROM products as p
                    JOIN product_properties pp on p.product_id = pp.product_id
                    JOIN property_values pv on pv.property_values_id = pp.property_values_id
                    JOIN properties p2 on p2.property_id = pv.property_id
                    WHERE p.title='{productTitle}' AND p2.title='{propertyTitle}';";

            await using var cmd = new NpgsqlCommand(sql, con);
            await using NpgsqlDataReader rdr = await cmd.ExecuteReaderAsync();
            if (!await rdr.ReadAsync())
                return null;
            
            var value = rdr.GetString(0);

            await con.CloseAsync();
            return value;
        }
    }
}