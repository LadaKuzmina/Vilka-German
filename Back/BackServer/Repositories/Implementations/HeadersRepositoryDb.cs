using System;
using System.Collections.Generic;
using System.Linq;
using BackServer.Contexts;
using DbEntity;

namespace BackServer.Repositories
{
    public class HeadersRepositoryDb : IHeadersRepository
    {
        private readonly TestContext _context;

        public HeadersRepositoryDb(TestContext context)
        {
            _context = context;
        }

        public IEnumerable<Entity.HeadingOne> GetAllHeadingsOne()
        {
            return _context.HeadingsOne.Select(x => new Entity.HeadingOne() {Title = x.Title});
        }

        public IEnumerable<Entity.HeadingTwo> GetAllHeadingsTwo()
        {
            return _context.HeadingsTwo.Select(x => new Entity.HeadingTwo() {Title = x.Title});
        }

        public IEnumerable<Entity.HeadingTwo> GetHeadingsTwoByHeadingsOne(Entity.HeadingOne heading)
        {
            return _context.HeadingsTwo
                .Where(x => x.HeadingOne.Title == heading.Title)
                .Select(x => new Entity.HeadingTwo() {Title = x.Title});
        }
    }
}