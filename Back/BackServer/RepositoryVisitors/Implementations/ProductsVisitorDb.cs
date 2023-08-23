using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackServer.Contexts;
using BackServer.RepositoryChangers.Implementations;
using BackServer.Services;
using BackServer.Services.Interfaces;
using DbEntity;
using NpgsqlDbExtensions;
using Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Npgsql;
using NpgsqlDbExtensions.Enums;
using Product = Entity.Product;

namespace BackServer.Repositories
{
    public class ProductsVisitorDb : IProductVisitor
    {
        private readonly TestContext _context;
        private readonly IPropertyVisitor _propertyVisitor;
        private readonly IPhotoVisitor _photoVisitor;

        public ProductsVisitorDb(TestContext context, IPropertyVisitor propertyVisitor, IPhotoVisitor photoVisitor)
        {
            _context = context;
            _propertyVisitor = propertyVisitor;
            _photoVisitor = photoVisitor;
        }

        public async Task<IEnumerable<Entity.Product>> GetAll()
        {
            var products = new List<Entity.Product>();
            await using var con = (NpgsqlConnection?) _context.Database.GetDbConnection();
            if (con.State != ConnectionState.Open)
                await con.OpenAsync();

            var sql = @$"
                SELECT p.title, p.description, p.price, p.quantity, p.popularity, p.available, p.page_link, um.unit_measurement_value,
                    hone.title, htwo.title, pv.property_value 
                FROM products as p
                         JOIN product_family pf on pf.product_family_id = p.product_family_id
                         JOIN units_measurement um on um.unit_measurement_id = pf.unit_measurement_id
                         JOIN heading_one hone on hone.heading_one_id = pf.heading_one_id
                         JOIN heading_two htwo on pf.heading_two_id=htwo.heading_two_id
                         LEFT JOIN heading_three hthree on p.heading_three_id=hthree.heading_three_id
                         LEFT JOIN property_values pv on hthree.property_values_id = pv.property_values_id";
            await using var cmd = new NpgsqlCommand(sql, con);
            await using NpgsqlDataReader rdr = await cmd.ExecuteReaderAsync();
            while (await rdr.ReadAsync())
            {
                products.Add(await ConvertProduct(rdr));
            }

            return products;
        }

        public async Task<IEnumerable<Entity.Product>> GetAvailable()
        {
            var products = new List<Entity.Product>();
            await using var con = (NpgsqlConnection?) _context.Database.GetDbConnection();
            if (con.State != ConnectionState.Open)
                await con.OpenAsync();

            var sql = @$"
                SELECT p.title, p.description, p.price, p.quantity, p.popularity, p.available, p.page_link, um.unit_measurement_value,
                    hone.title, htwo.title, pv.property_value 
                FROM products as p
                         JOIN product_family pf on pf.product_family_id = p.product_family_id
                         JOIN units_measurement um on um.unit_measurement_id = pf.unit_measurement_id
                         JOIN heading_one hone on hone.heading_one_id = pf.heading_one_id
                         JOIN heading_two htwo on pf.heading_two_id=htwo.heading_two_id
                         LEFT JOIN heading_three hthree on p.heading_three_id=hthree.heading_three_id
                         LEFT JOIN property_values pv on hthree.property_values_id = pv.property_values_id
                WHERE p.available=true;";
            await using var cmd = new NpgsqlCommand(sql, con);
            await using NpgsqlDataReader rdr = await cmd.ExecuteReaderAsync();
            while (await rdr.ReadAsync())
            {
                products.Add(await ConvertProduct(rdr));
            }

            return products;
        }

        public async Task<Entity.Product?> GetByTitle(string title)
        {
            Entity.Product product = default!;
            var con = (NpgsqlConnection?) _context.Database.GetDbConnection();
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
                             LEFT JOIN sales s on sp.sale_id = s.sale_id
                    WHERE LOWER(p.title)='{title.ToLower()}';";
            await using var cmd = new NpgsqlCommand(sql, con);
            await using NpgsqlDataReader rdr = await cmd.ExecuteReaderAsync();
            while (await rdr.ReadAsync())
            {
                product = new Entity.Product(rdr.GetString(0), rdr.GetString(1), rdr.GetInt32(2), rdr.GetInt32(3),
                    rdr.GetInt32(4), rdr.GetBoolean(5), await rdr.ReadNullOrStringAsync(6), rdr.GetString(7))
                {
                    HeadingOne = rdr.GetString(8),
                    HeadingTwo = rdr.GetString(9),
                    HeadingThree = await rdr.ReadNullOrStringAsync(10),
                    SalePrice = rdr.GetInt32(11)
                };
            }

            await con.CloseAsync();

            if (product is null)
            {
                return null;
            }
            
            product.ImageRefs = await _photoVisitor.GetAllProductPhoto(product.Title);

            return product;
        }

        public async Task<IEnumerable<Product>> GetBySubstring(string substring)
        {
            var products = new List<Entity.Product>();
            await using var con = (NpgsqlConnection?) _context.Database.GetDbConnection();
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
                             LEFT JOIN sales s on sp.sale_id = s.sale_id
                    WHERE LOWER(p.title) LIKE '%{substring.ToLower()}%';";
            await using var cmd = new NpgsqlCommand(sql, con);
            await using NpgsqlDataReader rdr = await cmd.ExecuteReaderAsync();
            while (await rdr.ReadAsync())
            {
                products.Add(await ConvertProduct(rdr));
            }

            return products;
        }

        public async Task<IEnumerable<Product>> GetBySubstrings(string[] substrings)
        {
            var productsDictionary = new Dictionary<Entity.Product, int>();
            
            await using var con = (NpgsqlConnection?)_context.Database.GetDbConnection();
            if (con.State != ConnectionState.Open)
                await con.OpenAsync();
            
            foreach (var substring in substrings.Select(substring => substring.ToLower()))
            {
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
                             LEFT JOIN sales s on sp.sale_id = s.sale_id
                    WHERE LOWER(p.title) LIKE '%{substring}%'
                       OR LOWER(hone.title) LIKE '%{substring}%'
                       OR LOWER(htwo.title) LIKE '%{substring}%'
                       OR LOWER(pv.property_value) LIKE '%{substring}%';";
                await using var cmd = new NpgsqlCommand(sql, con);
                await using NpgsqlDataReader rdr = await cmd.ExecuteReaderAsync();
                while (await rdr.ReadAsync())
                {
                    var product = await ConvertProduct(rdr);
                    productsDictionary.TryAdd(product, 0);
                    productsDictionary[product] += 1;
                }
            }

            return productsDictionary.Keys.Where(product => productsDictionary[product] >= substrings.Length).ToList();
        }

        public async Task<IEnumerable<Entity.Product>> GetAllHeadingOne(string headingOneTitle)
        {
            return await GetAllByHeading(headingOneTitle, Headings.HeadingOne);
        }

        public async Task<IEnumerable<Entity.Product>> GetAllHeadingTwo(string headingTwoTitle)
        {
            return await GetAllByHeading(headingTwoTitle, Headings.HeadingTwo);
        }

        public async Task<IEnumerable<Entity.Product>> GetPageHeadingOne(string headingOneTitle,
            ProductOrders productOrder, Dictionary<string, HashSet<string>> reqProperties, int pageNumber,
            int countElements)
        {
            var products = await GetHeadingsPage(headingOneTitle, Headings.HeadingOne, productOrder, reqProperties,
                pageNumber, countElements);
            await GetPriorityProperties(products);
            return products;
        }

        public async Task<IEnumerable<Entity.Product>> GetPageHeadingTwo(string headingTwoTitle,
            ProductOrders productOrder, Dictionary<string, HashSet<string>> reqProperties, int pageNumber,
            int countElements)
        {
            var products = await GetHeadingsPage(headingTwoTitle, Headings.HeadingTwo, productOrder, reqProperties,
                pageNumber, countElements);
            await GetPriorityProperties(products);
            return products;
        }

        public async Task<IEnumerable<Entity.Product>> GetPageHeadingThree(string headingThreeTitle,
            ProductOrders productOrder,
            Dictionary<string, HashSet<string>> reqProperties, int pageNumber, int countElements)
        {
            var products = await GetHeadingsPage(headingThreeTitle, Headings.HeadingThree, productOrder, reqProperties,
                pageNumber, countElements);
            await GetPriorityProperties(products);
            return products;
        }

        public async Task<int> GetCountPagesHeadingOne(string headingOneTitle, ProductOrders productOrder,
            Dictionary<string, HashSet<string>> reqProperties, int countElements)
        {
            var products = await GetHeadingsPage(headingOneTitle, Headings.HeadingOne, productOrder, reqProperties, 1,
                countElements);
            return products.Count;
        }

        public async Task<int> GetCountPagesHeadingTwo(string headingTwoTitle, ProductOrders productOrder,
            Dictionary<string, HashSet<string>> reqProperties, int countElements)
        {
            var products = await GetHeadingsPage(headingTwoTitle, Headings.HeadingTwo, productOrder, reqProperties, 1,
                countElements);
            return products.Count;
        }

        public async Task<List<Entity.Product>> GetHeadingsPage(string headingTitle, Headings heading,
            ProductOrders productOrder, Dictionary<string, HashSet<string>> reqProperties, int pageNumber,
            int countElements)
        {
            var con = (NpgsqlConnection?) _context.Database.GetDbConnection();

            if (con.State != ConnectionState.Open)
                await con.OpenAsync();

            var sql = new StringBuilder();
            sql.Append(@$"
                SELECT p.title, p.description, p.price, p.quantity, p.popularity, p.available, p.page_link,
                       um.unit_measurement_value,
                       CASE WHEN s.percent ISNULL THEN p.price ELSE p.price * (100-s.percent)/100 END
                FROM products as p
                         JOIN product_family pf on pf.product_family_id = p.product_family_id
                         JOIN units_measurement um on um.unit_measurement_id = pf.unit_measurement_id
                         LEFT JOIN sale_products sp on p.product_id = sp.product_id
                         LEFT JOIN sales s on sp.sale_id = s.sale_id");

            sql.Append($" {JoinByHeading(heading)}");
            sql.Append($" {WhereByHeading(heading, headingTitle)}");
            sql.Append($" {SelectOrder(productOrder)};");

            var products = await ExecuteSqlCommand(sql.ToString(), con);
            await con.CloseAsync();

            products = await GetByRequirements(products, reqProperties, (pageNumber - 1) * countElements,
                countElements);


            return products;
        }

        public async Task<List<Entity.Product>> GetAllByHeading(string headingTitle, Headings heading)
        {
            var con = (NpgsqlConnection?) _context.Database.GetDbConnection();

            if (con.State != ConnectionState.Open)
                await con.OpenAsync();

            var sql = new StringBuilder();
            sql.Append(@$"
                SELECT p.title, p.description, p.price, p.quantity, p.popularity, p.available, p.page_link,
                       um.unit_measurement_value,
                       CASE WHEN s.percent ISNULL THEN p.price ELSE p.price * (100-s.percent)/100 END
                FROM products as p
                         JOIN product_family pf on pf.product_family_id = p.product_family_id
                         JOIN units_measurement um on um.unit_measurement_id = pf.unit_measurement_id
                         LEFT JOIN sale_products sp on p.product_id = sp.product_id
                         LEFT JOIN sales s on sp.sale_id = s.sale_id");

            sql.Append($" {JoinByHeading(heading)}");
            sql.Append($" {WhereByHeading(heading, headingTitle)};");

            var products = await ExecuteSqlCommand(sql.ToString(), con);
            await con.CloseAsync();


            return products;
        }

        private string JoinByHeading(Headings heading)
        {
            switch (heading)
            {
                case Headings.HeadingOne:
                    return
                        $"JOIN heading_one ho on ho.heading_one_id = pf.heading_one_id";
                case Headings.HeadingTwo:
                    return
                        $"JOIN heading_two ht on ht.heading_two_id = pf.heading_two_id";
                case Headings.HeadingThree:
                    return
                        $@"JOIN heading_three ht on p.heading_three_id = ht.heading_three_id
                          JOIN property_values pv on ht.property_values_id = pv.property_values_id";
                default:
                    return "";
            }
        }

        private string WhereByHeading(Headings heading, string headingTitle)
        {
            switch (heading)
            {
                case Headings.HeadingOne:
                    return
                        $"Where p.available AND ho.title = '{headingTitle}'";
                case Headings.HeadingTwo:
                    return
                        $"WHERE p.available AND ht.title = '{headingTitle}'";
                case Headings.HeadingThree:
                    return $"WHERE p.available AND pv.property_value='{headingTitle}'";
                default:
                    return "";
            }
        }

        private string SelectOrder(ProductOrders productOrder)
        {
            switch (productOrder)
            {
                case ProductOrders.Popularity:
                    return "ORDER BY p.popularity";
                case ProductOrders.PopularityDesc:
                    return "ORDER BY p.popularity DESC";
                case ProductOrders.Price:
                    return
                        "ORDER BY CASE WHEN s.percent ISNULL THEN p.price ELSE p.price * (100 - s.percent) / 100 END";
                case ProductOrders.PriceDesc:
                    return
                        "ORDER BY CASE WHEN s.percent ISNULL THEN p.price ELSE p.price * (100 - s.percent) / 100 END DESC";
                case ProductOrders.Alphabet:
                    return "ORDER BY p.title";
                case ProductOrders.AlphabetDesc:
                    return "ORDER BY p.title DESC";
                default:
                    return "";
            }
        }

        private async Task<List<Entity.Product>> ExecuteSqlCommand(string sql, NpgsqlConnection? con)
        {
            var products = new List<Entity.Product>();
            await using var cmd = new NpgsqlCommand(sql, con);
            {
                await using NpgsqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var product = await ConvertProductWithSale(rdr);
                    products.Add(product);
                }
            }

            return products;
        }

        private async Task GetPriorityProperties(IEnumerable<Entity.Product> products)
        {
            foreach (var product in products)
                product.PriorityProperties = await _propertyVisitor.GetPriorityByProduct(product.Title);
        }

        private async Task<List<Entity.Product>> GetByRequirements(IEnumerable<Entity.Product> products,
            Dictionary<string, HashSet<string>> properties, int numberSkip, int numberTake)
        {
            var resultProducts = new List<Entity.Product>();
            foreach (var product in products)
            {
                var productProperties = await GetAllProperties(product);
                var meetsRequirements = true;

                if (properties.ContainsKey("Максимальная цена") &&
                    product.SalePrice > int.Parse(properties["Максимальная цена"].First()) ||
                    properties.ContainsKey("Минимальная цена") &&
                    product.SalePrice < int.Parse(properties["Минимальная цена"].First()))
                {
                    continue;
                }

                foreach (var pp in productProperties)
                {
                    if (properties.ContainsKey(pp.Title) && !properties[pp.Title].Contains(pp.Values.First()))
                    {
                        meetsRequirements = false;
                        break;
                    }
                }
                

                if (meetsRequirements)
                {
                    numberSkip--;
                    if (numberSkip < 0)
                        resultProducts.Add(product);
                }

                if (resultProducts.Count == numberTake)
                    break;
            }

            return resultProducts;
        }

        private async Task<IEnumerable<Entity.Property>> GetAllProperties(Entity.Product product)
        {
            return await _propertyVisitor.GetAllByProduct(product.Title);
        }

        private async Task<Entity.Product> ConvertProduct(NpgsqlDataReader rdr)
        {
            return new Entity.Product(rdr.GetString(0), rdr.GetString(1), rdr.GetInt32(2), rdr.GetInt32(3),
                rdr.GetInt32(4), rdr.GetBoolean(5), await rdr.ReadNullOrStringAsync(6), rdr.GetString(7))
            {
                HeadingOne = rdr.GetString(8),
                HeadingTwo = rdr.GetString(9),
                HeadingThree = await rdr.ReadNullOrStringAsync(10)
            };
        }

        private async Task<Entity.Product> ConvertProductWithSale(NpgsqlDataReader rdr)
        {
            return new Entity.Product(rdr.GetString(0), rdr.GetString(1), rdr.GetInt32(2), rdr.GetInt32(3),
                rdr.GetInt32(4), rdr.GetBoolean(5), await rdr.ReadNullOrStringAsync(6), rdr.GetString(7))
            {
                SalePrice = rdr.GetInt32(8)
            };
        }
    }
}