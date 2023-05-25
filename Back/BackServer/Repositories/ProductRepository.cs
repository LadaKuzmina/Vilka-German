using System.Collections.Generic;
using System.Linq;
using BackServer.Contexts;
using DbEntity;

namespace BackServer.Repositories
{
    public class ProductRepository
    {
        public IEnumerable<Product> GetAll()
        {
            using (TestContext db = new())
            {
                return db.Products;
            }
        }
        
        public IEnumerable<Entity.Product> GetByHeadingOne(HeadingOne heading)
        {
            using (TestContext db = new())
            {
                return db.Products
                    .Where(x => x.HeadingOne == heading)
                    .Join();
            }
        }
        
        public IEnumerable<Entity.Product> GetByHeadingTwo(HeadingTwo heading)
        {
            using (TestContext db = new())
            {
                return db.Products.Where(x => x.HeadingTwo == heading);
            }
        }
    } 
}