using System.Collections.Generic;
using System.Linq;
using BackServer.Contexts;
using DbEntity;
using Entity;

namespace BackServer.Repositories
{
    public class PropertiesRepository
    {
        public IEnumerable<string> GetAllTitles()
        {
            // using (TestContext db = new())
            // {
            //     return db.Properties.Select(x => x.Title);
            // }
            throw new NotImplementedException();
        }

        public IEnumerable<Entity.Property> GetByProduct(DbEntity.Product product)
        {
            // using (TestContext db = new())
            // {
            //     return db.ProductProperties
            //         .Where(x => x.ProductId == product.Id)
            //         .Join(db.Properties, cur => cur.PropertyId, other => other.Id,
            //             (pp, p) => new Entity.Property()
            //             {
            //                 Value = pp.PropertyValue,
            //                 Title = p.Title
            //             });
            // }
            throw new NotImplementedException();
        }

        public IEnumerable<Entity.Property> GetByHeadingTwo(DbEntity.HeadingTwo heading)
        {
            // using (TestContext db = new())
            // {
            //     return db.ProductProperties
            //         .Join(db.Products, cur => cur.ProductId, other => other.Id,
            //             (cur, other) => new
            //             {
            //                 other.HeadingTwo, cur.PropertyId, cur.PropertyValue
            //             })
            //         .Where(x => x.HeadingTwo == heading)
            //         .Join(db.Properties, cur => cur.PropertyId, other => other.Id, (cur, other) => new Entity.Property()
            //         {
            //             Value = cur.PropertyValue,
            //             Title = other.Title
            //         });
            // }
            throw new NotImplementedException();
        }
    }
}