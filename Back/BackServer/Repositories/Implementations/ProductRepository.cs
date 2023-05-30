using System;
using System.Collections.Generic;
using System.Linq;
using BackServer.Contexts;
using DbEntity;

namespace BackServer.Repositories
{
    public class ProductRepository
    {
        private readonly TestContext _context;

        public ProductRepository(TestContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Products;
        }

        public IEnumerable<Entity.Product> GetByHeadingOne(HeadingOne heading)
        {
            //     return _context.Products
            //         .Where(x => x.HeadingOne == heading)
            //         .Join();
            throw new NotImplementedException();
        }

        public IEnumerable<Entity.Product> GetByHeadingTwo(HeadingTwo heading)
        {
            //     return _context.Products.Where(x => x.HeadingTwo == heading).Select(x=>new Entity.Product(){HeadingOne = x.HeadingOne, HeadingTwo = x.HeadingTwo});
            throw new NotImplementedException();
        }
    }
}