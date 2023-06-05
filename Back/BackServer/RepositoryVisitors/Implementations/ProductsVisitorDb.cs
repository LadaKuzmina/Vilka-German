using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackServer.Contexts;
using DbEntity;
using Microsoft.EntityFrameworkCore;

namespace BackServer.Repositories
{
    public class ProductsVisitorDb : IProductVisitor
    {
        private readonly TestContext _context;

        public ProductsVisitorDb(TestContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Entity.Product>> GetAll()
        {
            return await _context.Products.Select(x => new Entity.Product()
                {Description = x.Description, Price = x.Price, Quantity = x.Quantity, Title = x.Title}).ToListAsync();
        }

        public async Task<IEnumerable<Entity.Product>> GetByHeadingOne(Entity.HeadingOne heading)
        {
            // return await _context.Products
            //     .Where(x => x.HeadingOne == heading)
            //     .Join();
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Entity.Product>> GetByHeadingTwo(Entity.HeadingTwo heading)
        {
            // return await _context.Products
            //     .Where(x => x.HeadingTwo == heading)
            //     .Select(x => new Entity.Product()
            //         {HeadingOne = x.HeadingOne, HeadingTwo = x.HeadingTwo});
            throw new NotImplementedException();
        }
    }
}