using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Xml;
using BackServer.Contexts;
using BackServer.Services.Interfaces;
using DbEntity;
using Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Internal;
using Npgsql;
using NpgsqlDbExtensions;
using NpgsqlDbExtensions.Enums;
using Property = Entity.Property;

namespace BackServer.Repositories
{
    public class PropertiesVisitorDb : IPropertyVisitor
    {
        private readonly TestContext _context;
        private readonly IHeadersVisitor _headersVisitor;

        public PropertiesVisitorDb(TestContext context, IHeadersVisitor headersVisitor)
        {
            _context = context;
            _headersVisitor = headersVisitor;
        }

        public async Task<IEnumerable<string>> GetAllTitles()
        {
            return await _context.Properties.Select(x => x.Title).ToArrayAsync();
        }

        public async Task<IEnumerable<Entity.Property>> GetAllByProduct(string productTitle)
        {
            var res = new List<Entity.Property>();
            var con = (NpgsqlConnection?)_context.Database.GetDbConnection();
            
            if (con.State != ConnectionState.Open)
                await con.OpenAsync();

            var sql = @$"
            SELECT prop.title, p1.property_value 
            FROM products AS p
                     JOIN product_properties pp on p.product_id = pp.product_id
                     JOIN property_values AS p1 ON pp.property_values_id = p1.property_values_id
                     JOIN properties as prop ON p1.property_id = prop.property_id
            WHERE p.title = '{productTitle}';";
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
                WHERE p.title= '{productTitle}';";

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

        public async Task<IEnumerable<Property>> GetBySubstrings(string[] substrings)
        {
            var products = await GetAllProductsBySubstrings(substrings);
            
            var properties = new List<Entity.Property>
            {
                new Entity.Property("Максимальная цена", new[] {products.Select(product => product.SalePrice).Max().ToString()}),
                new Entity.Property("Минимальная цена", new[] {products.Select(product => product.SalePrice).Min().ToString()})
            };

            var propertiesToAdd = new Dictionary<string, SortedSet<string>>();
            var keysCount = new Dictionary<string, int>();
            foreach (var product in products)
            {
                var productProperties = await GetAllByProduct(product.Title).ConfigureAwait(false);
                foreach (var productProperty in productProperties)
                {
                    propertiesToAdd.TryAdd(productProperty.Title, new SortedSet<string>());
                    keysCount.TryAdd(productProperty.Title, 0);
                    propertiesToAdd[productProperty.Title].Add(productProperty.Values.FirstOrDefault()!);
                    keysCount[productProperty.Title]++;
                }
            }

            properties.AddRange(propertiesToAdd
                .Where(propertyToAdd => keysCount[propertyToAdd.Key] == products.Count)
                .Select(propertyToAdd => new Entity.Property(propertyToAdd.Key, propertyToAdd.Value)));

            return properties;
        }

        public async Task<IEnumerable<Entity.Property>> GetByHeadingOne(string headingOneTitle)
        {
            var res = (await GetMaxMinPrice(headingOneTitle, Headings.HeadingOne)).ToList();
            var products = await GetAllByHeading(headingOneTitle, Headings.HeadingOne);
            var filters = await _headersVisitor.GetHeadingsOneFiltersAsync(headingOneTitle);

            foreach (var filter in filters)
            {
                var values = new HashSet<string>();
                foreach (var product in products)
                    values.Add(await GetProductPropertyValue(product.Title, filter));

                res.Add(new Entity.Property(filter, values));
            }

            return res;
        }

        public async Task<IEnumerable<Entity.Property>> GetByHeadingTwo(string headingTwoTitle)
        {
            var res = (await GetMaxMinPrice(headingTwoTitle, Headings.HeadingTwo)).ToList();
            var products = await GetAllByHeading(headingTwoTitle, Headings.HeadingTwo);
            var filters = await _headersVisitor.GetHeadingsTwoFiltersAsync(headingTwoTitle);

            foreach (var filter in filters)
            {
                var values = new HashSet<string>();
                foreach (var product in products)
                    values.Add(await GetProductPropertyValue(product.Title, filter));

                res.Add(new Entity.Property(filter, values));
            }

            return res;
        }

        public async Task<IEnumerable<Property>> GetBySubstringsAtLeastOne(string[] substrings)
        {
            var products = await GetAllProductsBySubstrings(substrings);
            
            var properties = new List<Entity.Property>
            {
                new Entity.Property("Максимальная цена", new[] {products.Select(product => product.SalePrice).Max().ToString()}),
                new Entity.Property("Минимальная цена", new[] {products.Select(product => product.SalePrice).Min().ToString()})
            };

            var propertiesToAdd = new Dictionary<string, SortedSet<string>>();
            var keysCount = new Dictionary<string, int>();
            foreach (var product in products)
            {
                var productProperties = await GetAllByProduct(product.Title).ConfigureAwait(false);
                foreach (var productProperty in productProperties)
                {
                    propertiesToAdd.TryAdd(productProperty.Title, new SortedSet<string>());
                    keysCount.TryAdd(productProperty.Title, 0);
                    propertiesToAdd[productProperty.Title].Add(productProperty.Values.FirstOrDefault()!);
                    keysCount[productProperty.Title]++;
                }
            }

            properties.AddRange(propertiesToAdd
                .Where(propertyToAdd => keysCount[propertyToAdd.Key] == products.Count)
                .Select(propertyToAdd => new Entity.Property(propertyToAdd.Key, propertyToAdd.Value)));

            return properties;
        }

        public async Task<IEnumerable<Property>> GetByHeadingOneAtLeastOne(string headingOneTitle)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Property>> GetByHeadingTwoAtLeastOne(string headingTwoTitle)
        {
            throw new NotImplementedException();
        }
        
        private async Task<IEnumerable<Property>> GetPropertiesByProductsAtLeastOne(Product[

        private async Task<IEnumerable<Entity.Property>> GetMaxMinPrice(string headingTitle, Headings heading)
        {
            var res = new List<Entity.Property>();
            var con = (NpgsqlConnection?) _context.Database.GetDbConnection();

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
                case Headings.HeadingThree:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(heading), heading, null);
            }
            
            if (con.State != ConnectionState.Open)
                await con.OpenAsync();
            await using var cmd = new NpgsqlCommand(sql.ToString(), con);
            await using NpgsqlDataReader rdr = await cmd.ExecuteReaderAsync();
            while (await rdr.ReadAsync())
            {
                res.Add(new Entity.Property("Максимальная цена", new[] {rdr.GetInt32(0).ToString()}));
                res.Add(new Entity.Property("Минимальная цена", new[] {rdr.GetInt32(1).ToString()}));
            }

            await con.CloseAsync();
            return res;
        }

        private async Task<List<Entity.Product>> GetAllByHeading(string headingTitle, Headings heading)
        {
            var products = new List<Entity.Product>();
            var con = (NpgsqlConnection?) _context.Database.GetDbConnection();

            var sql = new StringBuilder();
            sql.Append(@$"
                    SELECT p.title
                    FROM products as p
                    join product_family pf on pf.product_family_id = p.product_family_id");

            switch (heading)
            {
                case Headings.HeadingOne:
                    sql.Append($@" JOIN heading_one ho on ho.heading_one_id = pf.heading_one_id
                                WHERE ho.title='{headingTitle}';");
                    break;
                case Headings.HeadingTwo:
                    sql.Append($@" JOIN heading_two ht on ht.heading_two_id = pf.heading_two_id
                                WHERE ht.title='{headingTitle}';");
                    break;
            }

            if (con.State != ConnectionState.Open)
                await con.OpenAsync();
            
            await using var cmd = new NpgsqlCommand(sql.ToString(), con);
            await using NpgsqlDataReader rdr = await cmd.ExecuteReaderAsync();
            while (await rdr.ReadAsync())
            {
                products.Add(new Entity.Product(rdr.GetString(0)));
            }

            await con.CloseAsync();
            return products;
        }

        private async Task<List<Entity.Product>> GetAllProductsBySubstrings(string[] substrings)
        {
            var products = new List<Entity.Product>();
            
            var con = (NpgsqlConnection?)_context.Database.GetDbConnection();
            if (con.State != ConnectionState.Open)
                await con.OpenAsync();

            var sql = @$"
                SELECT p.title, p.description, p.price, p.quantity, p.popularity, p.available, p.page_link,
                       um.unit_measurement_value, hone.title, htwo.title, pv.property_value,
                       CASE WHEN s.percent ISNULL THEN p.price ELSE p.price * (100-s.percent)/100 END
                FROM products as p
                        JOIN product_family pf on pf.product_family_id = p.product_family_id
                        JOIN units_measurement um on um.unit_measurement_id = pf.unit_measurement_id
                        JOIN heading_one hone on hone.heading_one_id = pf.heading_one_id
                        JOIN heading_two htwo on pf.heading_two_id=htwo.heading_two_id
                        LEFT JOIN heading_three hthree on p.heading_three_id=hthree.heading_three_id
                        LEFT JOIN property_values pv on hthree.property_values_id = pv.property_values_id
                        LEFT JOIN sale_products sp on p.product_id = sp.product_id
                        LEFT JOIN sales s on sp.sale_id = s.sale_id;";
            
            await using var cmd = new NpgsqlCommand(sql, con);
            await using NpgsqlDataReader rdr = await cmd.ExecuteReaderAsync();
            while (await rdr.ReadAsync())
            {
                var product = new Entity.Product(rdr.GetString(0), rdr.GetString(1), rdr.GetInt32(2), rdr.GetInt32(3),
                    rdr.GetInt32(4), rdr.GetBoolean(5), await rdr.ReadNullOrStringAsync(6), rdr.GetString(7))
                {
                    HeadingOne = rdr.GetString(8),
                    HeadingTwo = rdr.GetString(9),
                    HeadingThree = await rdr.ReadNullOrStringAsync(10),
                    SalePrice = rdr.GetInt32(11)
                };
                
                products.Add(product);
            }

            await con.CloseAsync();
            
            products = products.Where(product => substrings
                .Select(substring => substring.ToLower())
                .All(substring => product.Title.ToLower().Contains(substring) 
                                  || product.HeadingOne.ToLower().Contains(substring)
                                  || product.HeadingTwo.ToLower().Contains(substring)
                                  || (product.HeadingThree != null && product.HeadingThree.ToLower()
                                      .Contains(substring)))).ToList();

            return products;
        }

        public async Task<string> GetProductPropertyValue(string productTitle, string propertyTitle)
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
                throw new ArgumentException(
                    $"У продукта {productTitle} не существует значения по свойству {propertyTitle}");
            
            var value = rdr.GetString(0);

            await con.CloseAsync();
            return value;
        }
    }
}