using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using BackServer.Contexts;
using DbEntity;
using Entity;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using NpgsqlDbExtensions;

namespace BackServer.Repositories
{
    public class ProjectsVisitorDb : IProjectVisitor
    {
        private readonly GsDbContext _context;

        public ProjectsVisitorDb(GsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Entity.Project>> GetAll()
        {
            return await _context.Projects
                .Select(x => new Entity.Project(x.Title, x.RoofType, x.PageLink))
                .ToListAsync();
        }

        public async Task<IEnumerable<Entity.Project>> GetRange(int pageNumber, int countElements)
        {
            var projects = new List<Entity.Project>();
            var con = (NpgsqlConnection?) _context.Database.GetDbConnection();
            if (con.State != ConnectionState.Open)
                await con.OpenAsync();

            var sql = @$"
                    SELECT p.title, p.roof_type, p.image_ref, p.page_link
                    FROM projects as p
                             JOIN project_materials pm on p.project_id = pm.project_id
                             JOIN products pr on pr.product_id = pm.product_id
                    ORDER BY p.priority DESC
                    OFFSET {(pageNumber - 1) * countElements}
                    LIMIT {countElements};";

            await using var cmd = new NpgsqlCommand(sql, con);
            {
                await using NpgsqlDataReader rdr = await cmd.ExecuteReaderAsync();
                while (await rdr.ReadAsync())
                {
                    var project = new Entity.Project(rdr.GetString(0), rdr.GetString(1), await rdr.ReadNullOrStringAsync(2));
                    projects.Add(project);
                }
            }

            await con.CloseAsync();

            return projects;
        }

        public async Task<int> GetCountPages(int countElements)
        {
            var c = await _context.Projects.CountAsync();
            var add = c % countElements != 0 ? 1 : 0;
            return c / countElements + add;
        }

        public async Task<IEnumerable<Entity.Product>> GetProductByProject(string projectTitle)
        {
            var products = new List<Entity.Product>();
            await using var con = (NpgsqlConnection?) _context.Database.GetDbConnection();
            if (con.State != ConnectionState.Open)
                await con.OpenAsync();

            var sql = @$"
                    SELECT pr.title
                    FROM projects as p
                             JOIN project_materials pm on p.project_id = pm.project_id
                             JOIN products pr on pr.product_id = pm.product_id
                    WHERE p.title= '{projectTitle}'
                    ORDER BY p.priority DESC;";

            await using var cmd = new NpgsqlCommand(sql, con);
            {
                await using NpgsqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var product = new Entity.Product(rdr.GetString(0));
                    products.Add(product);
                }
            }

            return products;
        }

        public async Task<IEnumerable<Entity.Project>> GetProjectByProduct(string productTitle)
        {
            var projects = new List<Entity.Project>();
            await using var con = (NpgsqlConnection?) _context.Database.GetDbConnection();
            if (con.State != ConnectionState.Open)
                await con.OpenAsync();

            var sql = @$"
                SELECT p.title, p.page_link
                FROM projects as p
                         JOIN project_materials pm on p.project_id = pm.project_id
                         JOIN products pr on pr.product_id = pm.product_id
                WHERE pr.title='{productTitle}'
                ORDER BY p.priority DESC;";

            await using var cmd = new NpgsqlCommand(sql, con);
            {
                await using NpgsqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var project = new Entity.Project(rdr.GetString(0), await rdr.ReadNullOrStringAsync(1));
                    projects.Add(project);
                }
            }

            return projects;
        }
    }
}