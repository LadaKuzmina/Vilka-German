using System.Collections.Generic;
using System.Linq;
using BackServer.Contexts;
using DbEntity;

namespace BackServer.Repositories
{
    public class HeadersRepository
    {
        public IEnumerable<HeadingOne> GetAllHeadingsOne()
        {
            using (TestContext db = new())
            {
                return db.HeadingsOne;
            }
        }

        public IEnumerable<HeadingTwo> GetAllHeadingsTwo()
        {
            using (TestContext db = new())
            {
                return db.HeadingsTwo;
            }
        }

        public IEnumerable<HeadingTwo> GetHeadingsTwoByHeadingsOne(HeadingOne heading)
        {
            using (TestContext db = new())
            {
                return db.HeadingsTwo.Where(x => x.HeadingOne == heading);
            }
        }
    }
}