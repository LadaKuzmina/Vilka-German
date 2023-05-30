using System;
using System.Collections.Generic;
using System.Linq;
using BackServer.Contexts;
using DbEntity;
using Entity;

namespace BackServer.Repositories
{
    public class PropertiesRepository
    {
        private readonly TestContext _context;

        public PropertiesRepository(TestContext context)
        {
            _context = context;
        }

        public IEnumerable<string> GetAllTitles()
        {
            return _context.Properties.Select(x => x.Title);
        }

        public IEnumerable<Entity.Property> GetByProduct(DbEntity.Product product)
        {
            return _context.ProductProperties
                .Where(x => x.ProductId == product.Id)
                .Join(_context.Properties, cur => cur.PropertyId, other => other.Id,
                    (pp, p) => new Entity.Property()
                    {
                        Value = pp.PropertyValue,
                        Title = p.Title
                    });
        }

        public IEnumerable<Entity.Property> GetByHeadingTwo(DbEntity.HeadingTwo heading)
        {
            return _context.ProductProperties
                .Join(_context.Products, cur => cur.ProductId, other => other.Id,
                    (cur, other) => new
                    {
                        other.HeadingTwo, cur.PropertyId, cur.PropertyValue
                    })
                .Where(x => x.HeadingTwo == heading)
                .Join(_context.Properties, cur => cur.PropertyId, other => other.Id, (cur, other) =>
                    new Entity.Property()
                    {
                        Value = cur.PropertyValue,
                        Title = other.Title
                    });
        }
    }
}