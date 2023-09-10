using System.Data;
using System.Text;
using BackServer.Contexts;
using BackServer.Repositories;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using NpgsqlDbExtensions;
using NpgsqlDbExtensions.Enums;
using NpgsqlTypes;
using Product = Entity.Product;

namespace BackServer.RepositoryVisitors.Implementations
{
    public class ProductsVisitorDb : IProductVisitor
    {
        private readonly GsDbContext _context;
        private readonly IPropertyVisitor _propertyVisitor;
        private readonly IPhotoVisitor _photoVisitor;

        private readonly string sqlGetAllProduct = @"
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
                             LEFT JOIN sales s on sp.sale_id = s.sale_id";

        public ProductsVisitorDb(GsDbContext context, IPropertyVisitor propertyVisitor, IPhotoVisitor photoVisitor)
        {
            _context = context;
            _propertyVisitor = propertyVisitor;
            _photoVisitor = photoVisitor;
        }

        public async Task<IEnumerable<Entity.Product>> GetAll()
        {
            await using var dbConnection = (NpgsqlConnection?) _context.Database.GetDbConnection();
            if (dbConnection.State != ConnectionState.Open)
                await dbConnection.OpenAsync();

            var products =
                await ExecuteSqlCommand($"{sqlGetAllProduct};", dbConnection, Array.Empty<NpgsqlParameter>());

            await dbConnection.CloseAsync();
            return products;
        }

        public async Task<IEnumerable<Entity.Product>> GetAvailable()
        {
            await using var dbConnection = (NpgsqlConnection?) _context.Database.GetDbConnection();
            if (dbConnection.State != ConnectionState.Open)
                await dbConnection.OpenAsync();

            var sql = $"{sqlGetAllProduct}\n WHERE p.available=true;";

            var products = await ExecuteSqlCommand(sql, dbConnection, Array.Empty<NpgsqlParameter>());

            await dbConnection.CloseAsync();
            return products;
        }

        public async Task<Entity.Product?> GetByTitle(string title)
        {
            var dbConnection = (NpgsqlConnection?) _context.Database.GetDbConnection();
            if (dbConnection.State != ConnectionState.Open)
                await dbConnection.OpenAsync();

            var sql = $"{sqlGetAllProduct}\n  WHERE p.title=@TITLE;";

            var parameters = new[]
            {
                new NpgsqlParameter() {ParameterName = "@TITLE", NpgsqlDbType = NpgsqlDbType.Text, Value = title}
            };

            var products = await ExecuteSqlCommand(sql, dbConnection, parameters);

            await dbConnection.CloseAsync();

            if (products.Count == 0)
                return null;

            var product = products[0];
            product.ImageRefs = (await _photoVisitor.GetAllProductPhoto(product.Title)).ToList();

            return product;
        }

        public async Task<IEnumerable<Product>> GetBySubstring(string substring)
        {
            var products = new List<Entity.Product>();
            await using var dbConnection = (NpgsqlConnection?) _context.Database.GetDbConnection();
            if (dbConnection.State != ConnectionState.Open)
                await dbConnection.OpenAsync();

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
            await using var cmd = new NpgsqlCommand(sql, dbConnection);
            await using NpgsqlDataReader rdr = await cmd.ExecuteReaderAsync();
            while (await rdr.ReadAsync())
            {
                products.Add(await ConvertProduct(rdr));
            }

            await dbConnection.CloseAsync();
            return products;
        }

        public async Task<IEnumerable<Product>> GetBySubstrings(string[] substrings)
        {
            var productsDictionary = new Dictionary<Entity.Product, int>();
            
            await using var dbConnection = (NpgsqlConnection?)_context.Database.GetDbConnection();
            if (dbConnection.State != ConnectionState.Open)
                await dbConnection.OpenAsync();
            
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
                await using var cmd = new NpgsqlCommand(sql, dbConnection);
                await using NpgsqlDataReader rdr = await cmd.ExecuteReaderAsync();
                while (await rdr.ReadAsync())
                {
                    var product = await ConvertProduct(rdr);
                    productsDictionary.TryAdd(product, 0);
                    productsDictionary[product] += 1;
                }
            }

            await dbConnection.CloseAsync();
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
            var products = (await GetAllHeadingOne(headingOneTitle)).ToList();
            products = await GetByRequirements(products, reqProperties, 0, products.Count());
            return products.Count / countElements + (products.Count() % countElements == 0 ? 0 : 1);
        }

        public async Task<int> GetCountPagesHeadingTwo(string headingTwoTitle, ProductOrders productOrder,
            Dictionary<string, HashSet<string>> reqProperties, int countElements)
        {
            var products = (await GetAllHeadingTwo(headingTwoTitle)).ToList();
            products = await GetByRequirements(products, reqProperties, 0, products.Count());
            return products.Count / countElements + (products.Count() % countElements == 0 ? 0 : 1);
        }

        public async Task<List<Entity.Product>> GetHeadingsPage(string headingTitle, Headings heading,
            ProductOrders productOrder, Dictionary<string, HashSet<string>> reqProperties, int pageNumber,
            int countElements)
        {
            var dbConnection = (NpgsqlConnection?) _context.Database.GetDbConnection();

            if (dbConnection.State != ConnectionState.Open)
                await dbConnection.OpenAsync();

            var sql = new StringBuilder();
            sql.Append(sqlGetAllProduct);
            sql.Append($" {JoinByHeading(heading)}");
            sql.Append($" {WhereByHeading(heading)}");
            sql.Append($" {SelectOrder(productOrder)};");

            var parameters = new[]
            {
                new NpgsqlParameter() {ParameterName = "@TITLE", NpgsqlDbType = NpgsqlDbType.Text, Value = headingTitle}
            };

            var products = await ExecuteSqlCommand(sql.ToString(), dbConnection, parameters);
            await dbConnection.CloseAsync();

            products = await GetByRequirements(products, reqProperties, (pageNumber - 1) * countElements,
                countElements);
            foreach (var product in products)
            {
                product.ImageRefs = new List<string?>() {await _photoVisitor.GetPrimaryProductPhoto(product.Title)};
            }
            return products;
        }

        public async Task<List<Entity.Product>> GetAllByHeading(string headingTitle, Headings heading)
        {
            var dbConnection = (NpgsqlConnection?) _context.Database.GetDbConnection();

            if (dbConnection.State != ConnectionState.Open)
                await dbConnection.OpenAsync();

            var sql = new StringBuilder();
            sql.Append(sqlGetAllProduct);
            sql.Append($" {JoinByHeading(heading)}");
            sql.Append($" {WhereByHeading(heading)};");

            var parameters = new[]
            {
                new NpgsqlParameter() {ParameterName = "@TITLE", NpgsqlDbType = NpgsqlDbType.Text, Value = headingTitle}
            };

            var products = await ExecuteSqlCommand(sql.ToString(), dbConnection, parameters);
            await dbConnection.CloseAsync();


            return products;
        }

        private string JoinByHeading(Headings heading)
        {
            switch (heading)
            {
                case Headings.HeadingOne:
                    return
                        "JOIN heading_one ho on ho.heading_one_id = pf.heading_one_id";
                case Headings.HeadingTwo:
                    return
                        "JOIN heading_two ht on ht.heading_two_id = pf.heading_two_id";
                case Headings.HeadingThree:
                    return
                        @"JOIN heading_three ht on p.heading_three_id = ht.heading_three_id
                          JOIN property_values pv on ht.property_values_id = pv.property_values_id";
                default:
                    return "";
            }
        }

        private string WhereByHeading(Headings heading)
        {
            switch (heading)
            {
                case Headings.HeadingOne:
                    return
                        "Where p.available AND ho.title = @TITLE";
                case Headings.HeadingTwo:
                    return
                        "WHERE p.available AND ht.title = @TITLE";
                case Headings.HeadingThree:
                    return "WHERE p.available AND pv.property_value=@TITLE";
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

        private async Task<List<Product>> ExecuteSqlCommand(string sql, NpgsqlConnection? dbConnection,
            IEnumerable<NpgsqlParameter> parameters)
        {
            var products = new List<Entity.Product>();
            await using var command = new NpgsqlCommand(sql, dbConnection);
            {
                NpgsqlFunctions.AddParameters(command, parameters);
                await using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var product = await ConvertProduct(reader);
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

        private async Task<Entity.Product> ConvertProduct(NpgsqlDataReader reader)
        {
            return new Entity.Product(reader.GetString(0), reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3),
                reader.GetInt32(4), reader.GetBoolean(5), await reader.ReadNullOrStringAsync(6), reader.GetString(7))
            {
                HeadingOne = reader.GetString(8),
                HeadingTwo = reader.GetString(9),
                HeadingThree = await reader.ReadNullOrStringAsync(10),
                SalePrice = reader.GetInt32(11)
            };
        }
    }
}