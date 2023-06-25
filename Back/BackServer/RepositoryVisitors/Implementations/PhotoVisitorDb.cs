using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using BackServer.Contexts;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace BackServer.Repositories
{
    public class PhotoVisitorDb : IPhotoVisitor
    {
        private readonly TestContext _context;

        public PhotoVisitorDb(TestContext context)
        {
            _context = context;
        }

        public async Task<string?> GetPrimaryProductPhoto(string productTitle)
        {
            var con = (NpgsqlConnection?) _context.Database.GetDbConnection();

            if (con.State != ConnectionState.Open)
                await con.OpenAsync();

            var sql = @$"
                    SELECT pi.image_ref
                    FROM project_images as pi
                             JOIN projects p on pi.project_id = p.project_id
                    WHERE p.title = '{productTitle}' AND pi.is_primary
                    LIMIT 1;";

            await using var cmd = new NpgsqlCommand(sql, con);
            await using NpgsqlDataReader rdr = cmd.ExecuteReader();
            if (!await rdr.ReadAsync()) return null;
            var imageRefs = rdr.GetString(0);

            await con.CloseAsync();

            return imageRefs;
        }

        public async Task<IEnumerable<string>> GetAllProductPhoto(string productTitle)
        {
            var imageRefs = new List<string>();
            var con = (NpgsqlConnection?) _context.Database.GetDbConnection();
            if (con.State != ConnectionState.Open)
                await con.OpenAsync();

            var sql = @$"
                    SELECT pi.image_ref
                    FROM project_images as pi
                             JOIN projects p on pi.project_id = p.project_id
                    WHERE p.title = '{productTitle}';";

            await using var cmd = new NpgsqlCommand(sql, con);
            {
                await using NpgsqlDataReader rdr = await cmd.ExecuteReaderAsync();
                while (await rdr.ReadAsync())
                {
                    imageRefs.Add(rdr.GetString(0));
                }
            }

            await con.CloseAsync();

            return imageRefs;
        }

        public async Task<string?> GetPrimaryProjectPhoto(string projectTitle)
        {
            await using var con = (NpgsqlConnection?) _context.Database.GetDbConnection();
            if (con.State != ConnectionState.Open)
                await con.OpenAsync();

            var sql = @$"
                    SELECT pi.image_ref
                    FROM project_images as pi
                             JOIN projects p on pi.project_id = p.project_id
                    WHERE p.title = '{projectTitle}' AND pi.is_primary
                    LIMIT 1;";

            await using var cmd = new NpgsqlCommand(sql, con);
            await using NpgsqlDataReader rdr = cmd.ExecuteReader();
            if (!await rdr.ReadAsync()) return null;
            var imageRefs = rdr.GetString(0);

            return imageRefs;
        }


        public async Task<IEnumerable<string>> GetAllProjectPhoto(string projectTitle)
        {
            var imageRefs = new List<string>();
            await using var con = (NpgsqlConnection?) _context.Database.GetDbConnection();
            if (con.State != ConnectionState.Open)
                await con.OpenAsync();

            var sql = @$"
                    SELECT pi.image_ref
                    FROM project_images as pi
                             JOIN projects p on pi.project_id = p.project_id
                    WHERE p.title = '{projectTitle}';";

            await using var cmd = new NpgsqlCommand(sql, con);
            {
                await using NpgsqlDataReader rdr = await cmd.ExecuteReaderAsync();
                while (await rdr.ReadAsync())
                {
                    imageRefs.Add(rdr.GetString(0));
                }
            }

            return imageRefs;
        }
    }
}