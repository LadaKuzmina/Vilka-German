using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackServer.Contexts;
using BackServer.RepositoryChangers.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BackServer.RepositoryChangers.Implementations
{
    public class HeadersChangerDb : IHeadersChanger
    {
        private readonly TestContext _context;

        public HeadersChangerDb(TestContext context)
        {
            _context = context;
        }

        public async Task AddHeadingOne(Entity.HeadingOne headingOne)
        {
            //await _context.HeadingsOne.AddAsync(headingOne);
            await _context.SaveChangesAsync();
        }

        public async Task AddHeadingTwo(Entity.HeadingTwo headingTwo)
        {
            //await _context.HeadingsTwo.AddAsync(headingTwo);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteHeadingOne(int id)
        {
            using var transactionAsync = _context.Database.BeginTransactionAsync();
            try
            {
                await _context.HeadingsOne.Where(x => x.Id == id).ExecuteDeleteAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteHeadingTwo(int id)
        {
            using var transactionAsync = _context.Database.BeginTransactionAsync();
            try
            {
                await _context.HeadingsTwo.Where(x => x.Id == id).ExecuteDeleteAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            await _context.SaveChangesAsync();
        }
    }
}