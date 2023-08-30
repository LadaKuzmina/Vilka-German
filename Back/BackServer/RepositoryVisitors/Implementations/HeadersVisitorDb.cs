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
using HeadingOne = Entity.HeadingOne;
using HeadingThree = Entity.HeadingThree;
using HeadingTwo = Entity.HeadingTwo;

namespace BackServer.Repositories
{
    public class HeadersVisitorDb : IHeadersVisitor
    {
        private readonly TestContext _context;

        public HeadersVisitorDb(TestContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Entity.HeadingOne>> GetAllHeadingsOneAsync()
        {
            return await _context.HeadingsOne
                .Select(x => new Entity.HeadingOne(x.Title, x.PageLink))
                .ToListAsync();
        }

        public async Task<IEnumerable<Entity.HeadingTwo>> GetAllHeadingsTwoAsync()
        {
            return await _context.HeadingsTwo
                .Select(x => new Entity.HeadingTwo(x.Title, x.ImageRef, x.PageLink))
                .ToListAsync();
        }

        public async Task<IEnumerable<Entity.HeadingThree>> GetAllHeadingsThree()
        {
            return await _context.HeadingsThree
                .Select(x => new Entity.HeadingThree(x.PropertyValues.PropertyValue, x.ImageRef, x.PageLink))
                .ToListAsync();
        }

        public async Task<IEnumerable<Entity.HeadingTwo>> GetHeadingsTwoByHeadingsOneAsync(string headingOneTitle)
        {
            var a = _context.HeadingsTwo;
            
            return await _context.HeadingsTwo
                .Where(x => x.HeadingOne.Title == headingOneTitle)
                .Select(x => new Entity.HeadingTwo(x.Title, x.PageLink, x.ImageRef))
                .ToListAsync();
        }

        public async Task<IEnumerable<Entity.HeadingThree>> GetHeadingsThreeByHeadingsTwoAsync(string headingTwoTitle)
        {
            return await _context.HeadingsThree
                .Where(x => x.HeadingTwo.Title == headingTwoTitle)
                .Select(x => new Entity.HeadingThree(x.PropertyValues.PropertyValue, x.PageLink, x.ImageRef))
                .ToListAsync();
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
            var headingsThreeDictionary = new Dictionary<Entity.HeadingThree, int>();
            
            foreach (var substring in substrings.Select(substring => substring.ToLower()))
            {
                var headingsThree = await _context.HeadingsThree
                    .Where(x => x.PropertyValues.PropertyValue.ToLower().Contains(substring))
                    .Select(x => new Entity.HeadingThree(x.PropertyValues.PropertyValue, x.ImageRef, x.PageLink))
                    .ToListAsync();
                
                foreach (var headingThree in headingsThree)
                {
                    headingsThreeDictionary.TryAdd(headingThree, 0);
                    headingsThreeDictionary[headingThree] += 1;
                }
            }
            
            return headingsThreeDictionary.Keys.Where(headingThree => headingsThreeDictionary[headingThree] >= substrings.Length).ToList();
        }

        public async Task<IEnumerable<string>> GetHeadingsOneFiltersAsync(string headingOneTitle)
        {
            var sql = @$"
                    SELECT p.title
                    FROM heading_one as ho
                    JOIN heading_one_filters hof on ho.heading_one_id = hof.heading_one_id
                    JOIN properties p on hof.property_id = p.property_id
                    WHERE ho.title='{headingOneTitle}';";
            
            return await GetFilters(sql);
        }

        public async Task<IEnumerable<string>> GetHeadingsTwoFiltersAsync(string headingTwoTitle)
        {
            var sql = @$"
                    SELECT p.title
                    FROM heading_two as ht
                    JOIN heading_two_filters htf on ht.heading_two_id = htf.heading_two_id
                    JOIN properties p on htf.property_id = p.property_id
                    WHERE ht.title='{headingTwoTitle}';";
            
            return await GetFilters(sql);
        }

        private async Task<IEnumerable<string>> GetFilters(string sql)
        {
            var propertyTitles = new List<string>();
            var con = (NpgsqlConnection?) _context.Database.GetDbConnection();

            if (con.State != ConnectionState.Open)
                await con.OpenAsync();
            
            await using var cmd = new NpgsqlCommand(sql, con);
            {
                await using NpgsqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    propertyTitles.Add(rdr.GetString(0));
                }
            }

            await con.CloseAsync();
            return propertyTitles;
        }
    }
}