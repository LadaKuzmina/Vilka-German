using System.Collections.Generic;
using Entity;

namespace BackServer.Repositories
{
    public interface IHeadersRepository
    {
        IEnumerable<HeadingOne> GetAllHeadingsOne();
        IEnumerable<HeadingTwo> GetAllHeadingsTwo();
        IEnumerable<HeadingTwo> GetHeadingsTwoByHeadingsOne(HeadingOne heading);
    }
}