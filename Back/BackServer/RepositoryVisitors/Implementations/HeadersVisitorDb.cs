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
using NpgsqlTypes;
using HeadingOne = Entity.HeadingOne;
using HeadingThree = Entity.HeadingThree;
using HeadingTwo = Entity.HeadingTwo;

namespace BackServer.Repositories
{
    public class HeadersVisitorDb : IHeadersVisitor
    {
        private readonly GsDbContext _context;

        private readonly string getAllHeadingOne = "SELECT title, page_link, image_ref FROM heading_one";

        private readonly string getAllHeadingTwo = @"
            SELECT ht.title, ho.title, ht.image_ref, ht.page_link, ht.is_visible
            FROM heading_two ht 
            JOIN public.heading_one ho on ho.heading_one_id = ht.heading_one_id";

        private readonly string getAllHeadingThree = @"
        SELECT pv.property_value, ht.title, hth.image_ref, hth.page_link
        FROM heading_three hth
        JOIN property_values pv on pv.property_values_id = hth.property_values_id
        JOIN public.heading_two ht on ht.heading_two_id = hth.heading_two_id";

        public HeadersVisitorDb(GsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Entity.HeadingOne>> GetAllHeadingsOneAsync()
        {
            var dbConnection = (NpgsqlConnection?) _context.Database.GetDbConnection();
            if (dbConnection.State != ConnectionState.Open)
                await dbConnection.OpenAsync();

            var headingsOne =
                await ExecuteSqlCommandHeadingOne($"{getAllHeadingOne} ORDER BY heading_one_id;", dbConnection, Array.Empty<NpgsqlParameter>());

            await dbConnection.CloseAsync();

            return headingsOne;
        }

        public async Task<IEnumerable<Entity.HeadingTwo>> GetAllHeadingsTwoAsync()
        {
            var dbConnection = (NpgsqlConnection?) _context.Database.GetDbConnection();
            if (dbConnection.State != ConnectionState.Open)
                await dbConnection.OpenAsync();

            var headingsTwo =
                await ExecuteSqlCommandHeadingTwo($"{getAllHeadingTwo} ORDER BY heading_two_id;", dbConnection, Array.Empty<NpgsqlParameter>());
            
            await dbConnection.CloseAsync();
            
            return headingsTwo;
        }

        public async Task<IEnumerable<Entity.HeadingThree>> GetAllHeadingsThree()
        {
            var dbConnection = (NpgsqlConnection?) _context.Database.GetDbConnection();
            if (dbConnection.State != ConnectionState.Open)
                await dbConnection.OpenAsync();

            var headingsThree =
                await ExecuteSqlCommandHeadingThree($"{getAllHeadingThree} ORDER BY heading_three_id;", dbConnection,
                    Array.Empty<NpgsqlParameter>());
            
            await dbConnection.CloseAsync();

            return headingsThree;
        }

        public async Task<IEnumerable<Entity.HeadingTwo>> GetHeadingsTwoByHeadingsOneAsync(string headingOneTitle)
        {
            var dbConnection = (NpgsqlConnection?) _context.Database.GetDbConnection();
            if (dbConnection.State != ConnectionState.Open)
                await dbConnection.OpenAsync();

            var sql = $"{getAllHeadingTwo} WHERE ho.title=@TITLE ORDER BY heading_two_id;";
            var parameters = new[]
            {
                new NpgsqlParameter()
                    {ParameterName = "@TITLE", NpgsqlDbType = NpgsqlDbType.Text, Value = headingOneTitle}
            };

            var headingsTwo =
                await ExecuteSqlCommandHeadingTwo(sql, dbConnection, parameters);
            
            await dbConnection.CloseAsync();

            return headingsTwo.Where(headingTwo => headingTwo.IsVisible);
        }

        public async Task<IEnumerable<Entity.HeadingThree>> GetHeadingsThreeByHeadingsTwoAsync(string headingTwoTitle)
        { 
            var dbConnection = (NpgsqlConnection?) _context.Database.GetDbConnection();
            if (dbConnection.State != ConnectionState.Open)
                await dbConnection.OpenAsync();

            var sql = $"{getAllHeadingThree} WHERE ht.title=@TITLE ORDER BY heading_three_id;";
            var parameters = new[]
            {
                new NpgsqlParameter()
                    {ParameterName = "@TITLE", NpgsqlDbType = NpgsqlDbType.Text, Value = headingTwoTitle}
            };

            var headingsThree =
                await ExecuteSqlCommandHeadingThree(sql, dbConnection, parameters);
            
            await dbConnection.CloseAsync();

            return headingsThree;
        }

        public async Task<IEnumerable<HeadingOne>> GetHeadingsOneBySubstringsAsync(string[] substrings)
        {
            var headingsOneDictionary = new Dictionary<Entity.HeadingOne, int>();
                        
            foreach (var substring in substrings.Select(substring => substring.ToLower()))
            {
                var headingsOne = await _context.HeadingsOne
                    .Where(x => x.Title.ToLower().Contains(substring))
                    .Select(x => new Entity.HeadingOne(x.Title, x.PageLink))
                    .ToListAsync();
                
                foreach (var headingOne in headingsOne)
                {
                    headingsOneDictionary.TryAdd(headingOne, 0);
                    headingsOneDictionary[headingOne] += 1;
                }
            }
            
            return headingsOneDictionary.Keys.Where(headingOne => headingsOneDictionary[headingOne] >= substrings.Length).ToList();
        }

        public async Task<IEnumerable<HeadingTwo>> GetHeadingsTwoBySubstringsAsync(string[] substrings)
        {
            var headingsTwoDictionary = new Dictionary<Entity.HeadingTwo, int>();
                        
            foreach (var substring in substrings.Select(substring => substring.ToLower()))
            {
                var headingsTwo = await _context.HeadingsTwo
                    .Where(x => x.Title.ToLower().Contains(substring))
                    .Select(x => new Entity.HeadingTwo(x.Title, x.ImageRef, x.PageLink))
                    .ToListAsync();
                
                foreach (var headingTwo in headingsTwo)
                {
                    headingsTwoDictionary.TryAdd(headingTwo, 0);
                    headingsTwoDictionary[headingTwo] += 1;
                }
            }
            
            return headingsTwoDictionary.Keys.Where(headingTwo => headingsTwoDictionary[headingTwo] >= substrings.Length).ToList();
        }

        public async Task<IEnumerable<HeadingThree>> GetHeadingsThreeBySubstringsAsync(string[] substrings)
        {
            // var headingsThreeDictionary = new Dictionary<Entity.HeadingThree, int>();
            //             
            // foreach (var substring in substrings.Select(substring => substring.ToLower()))
            // {
            //     var headingsThree = await _context.HeadingsThree
            //         .Where(x => x.PropertyValues.PropertyValue.ToLower().Contains(substring))
            //         .Select(x => new Entity.HeadingThree(x.PropertyValues.PropertyValue, x.ImageRef, x.PageLink))
            //         .ToListAsync();
            //     
            //     foreach (var headingThree in headingsThree)
            //     {
            //         headingsThreeDictionary.TryAdd(headingThree, 0);
            //         headingsThreeDictionary[headingThree] += 1;
            //     }
            // }
            //
            // return headingsThreeDictionary.Keys.Where(headingThree => headingsThreeDictionary[headingThree] >= substrings.Length).ToList();
            throw new NotImplementedException();
        }

        private async Task<IEnumerable<Entity.HeadingOne>> ExecuteSqlCommandHeadingOne(string sql,
            NpgsqlConnection dbConnection, IEnumerable<NpgsqlParameter> parameters)
        {
            var headingsOne = new List<Entity.HeadingOne>();
            await using var command = new NpgsqlCommand(sql, dbConnection);
            {
                NpgsqlFunctions.AddParameters(command, parameters);
                await using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var headingOne = ConvertHeadingOne(reader);
                    headingsOne.Add(await headingOne);
                }
            }

            return headingsOne;
        }

        private async Task<IEnumerable<Entity.HeadingTwo>> ExecuteSqlCommandHeadingTwo(string sql,
            NpgsqlConnection dbConnection, IEnumerable<NpgsqlParameter> parameters)
        {
            var headingsTwo = new List<Entity.HeadingTwo>();
            await using var command = new NpgsqlCommand(sql, dbConnection);
            {
                NpgsqlFunctions.AddParameters(command, parameters);
                await using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var headingTwo = ConvertHeadingTwo(reader);
                    headingsTwo.Add(await headingTwo);
                }
            }

            return headingsTwo;
        }

        private async Task<IEnumerable<Entity.HeadingThree>> ExecuteSqlCommandHeadingThree(string sql,
            NpgsqlConnection dbConnection, IEnumerable<NpgsqlParameter> parameters)
        {
            var headingsThree = new List<Entity.HeadingThree>();
            await using var command = new NpgsqlCommand(sql, dbConnection);
            {
                NpgsqlFunctions.AddParameters(command, parameters);
                await using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var headingThree = ConvertHeadingThree(reader);
                    headingsThree.Add(await headingThree);
                }
            }

            return headingsThree;
        }

        private async Task<Entity.HeadingOne> ConvertHeadingOne(NpgsqlDataReader reader)
        {
            return new Entity.HeadingOne(reader.GetString(0), await reader.ReadNullOrStringAsync(1), await reader.ReadNullOrStringAsync(2));
        }

        private async Task<Entity.HeadingTwo> ConvertHeadingTwo(NpgsqlDataReader reader)
        {
            return new Entity.HeadingTwo(reader.GetString(0), await reader.ReadNullOrStringAsync(2),
                    await reader.ReadNullOrStringAsync(3))
                {HeadingOneTitle = reader.GetString(1), IsVisible = reader.GetBoolean(4)};
        }

        private async Task<Entity.HeadingThree> ConvertHeadingThree(NpgsqlDataReader reader)
        {
            return new Entity.HeadingThree(reader.GetString(0), reader.GetString(1),
                await reader.ReadNullOrStringAsync(2),
                await reader.ReadNullOrStringAsync(3));
        }
    }
}