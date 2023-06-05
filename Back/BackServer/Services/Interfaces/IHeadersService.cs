using System.Collections.Generic;
using System.Threading.Tasks;
using BackServer.RepositoryChangers.Interfaces;
using Entity;

namespace BackServer.Services.Interfaces
{
    public interface IHeadersService
    {
        Task<IEnumerable<HeadingOne>> GetAllHeadingsOne();

        Task<IEnumerable<Entity.HeadingTwo>> GetAllHeadingsTwo();

        Task<IEnumerable<Entity.HeadingTwo>> GetHeadingsTwoByHeadingsOne(string headingOneTitle);
    }
}