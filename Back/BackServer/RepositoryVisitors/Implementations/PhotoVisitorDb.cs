using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using BackServer.Contexts;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using NpgsqlDbExtensions;
using NpgsqlTypes;

namespace BackServer.Repositories
{
    public class PhotoVisitorDb : IPhotoVisitor
    {
        private readonly GsDbContext _context;

        private readonly string getAllProjectImage = @"SELECT pi.image_ref FROM project_images as pi JOIN projects p on pi.project_id = p.project_id";
        private readonly string getAllProductImage =  @"SELECT pi.image_ref FROM product_images as pi JOIN products p on pi.product_id = p.product_id";

        public PhotoVisitorDb(GsDbContext context)
        {
            _context = context;
        }

        public async Task<string?> GetPrimaryProductPhoto(string productTitle)
        {
            var dbConnection = (NpgsqlConnection?) _context.Database.GetDbConnection();

            if (dbConnection.State != ConnectionState.Open)
                await dbConnection.OpenAsync();

            var sql = $"{getAllProductImage} WHERE p.title = @TITLE AND pi.is_primary LIMIT 1;";

            await using var cmd = new NpgsqlCommand(sql, dbConnection);
            var parameters = new[]
            {
                new NpgsqlParameter() {ParameterName = "@TITLE", NpgsqlDbType = NpgsqlDbType.Text, Value = productTitle}
            };
            
            var imageRefs = await ExecuteSqlCommand(sql, dbConnection, parameters);

            await dbConnection.CloseAsync();

            return imageRefs.First();
        }

        public async Task<IEnumerable<string>> GetAllProductPhoto(string productTitle)
        {
            var dbConnection = (NpgsqlConnection?) _context.Database.GetDbConnection();
            if (dbConnection.State != ConnectionState.Open)
                await dbConnection.OpenAsync();

            var sql = $"{getAllProductImage} WHERE p.title = @TITLE;";

            var parameters = new[]
            {
                new NpgsqlParameter() {ParameterName = "@TITLE", NpgsqlDbType = NpgsqlDbType.Text, Value = productTitle}
            };
            
            var imageRefs = await ExecuteSqlCommand(sql, dbConnection, parameters);

            await dbConnection.CloseAsync();

            return imageRefs;
        }

        public async Task<string?> GetPrimaryProjectPhoto(string projectTitle)
        {
           var dbConnection = (NpgsqlConnection?) _context.Database.GetDbConnection();
            if (dbConnection.State != ConnectionState.Open)
                await dbConnection.OpenAsync();

            var sql = $"{getAllProjectImage} WHERE p.title = @TITLE AND pi.is_primary LIMIT 1;";

            var parameters = new[]
            {
                new NpgsqlParameter() {ParameterName = "@TITLE", NpgsqlDbType = NpgsqlDbType.Text, Value = projectTitle}
            };
            
            var imageRefs = await ExecuteSqlCommand(sql, dbConnection, parameters);
            await dbConnection.CloseAsync();

            return imageRefs.First();
        }


        public async Task<IEnumerable<string>> GetAllProjectPhoto(string projectTitle)
        {
            var dbConnection = (NpgsqlConnection?) _context.Database.GetDbConnection();
            if (dbConnection.State != ConnectionState.Open)
                await dbConnection.OpenAsync();

            var sql = $"{getAllProjectImage} WHERE p.title = @TITLE AND pi.is_primary LIMIT 1;";

            var parameters = new[]
            {
                new NpgsqlParameter() {ParameterName = "@TITLE", NpgsqlDbType = NpgsqlDbType.Text, Value = projectTitle}
            };
            
            var imageRefs = await ExecuteSqlCommand(sql, dbConnection, parameters);
            await dbConnection.CloseAsync();

            return imageRefs;
        }
        
        private async Task<IEnumerable<string>> ExecuteSqlCommand(string sql,
            NpgsqlConnection dbConnection, IEnumerable<NpgsqlParameter> parameters)
        {
            var imageRefs = new List<string>();
            await using var command = new NpgsqlCommand(sql, dbConnection);
            {
                NpgsqlFunctions.AddParameters(command, parameters);
                await using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    imageRefs.Add( reader.GetString(0));
                }
            }

            return imageRefs;
        }
    }
}