using System.Collections.Generic;
using System.Threading.Tasks;
using BackServer.Repositories;
using BackServer.RepositoryChangers.Interfaces;
using BackServer.Services.Interfaces;

namespace BackServer.Services
{
    public class HeadersService : IHeadersService
    {
        private readonly IHeadersVisitor _visitor;
        private readonly IHeadersChanger _changer;
        public HeadersService(IHeadersVisitor visitor, IHeadersChanger changer)
        {
            _visitor = visitor;
            _changer = changer;
        }
        
        public async Task<IEnumerable<Entity.HeadingOne>> GetAllHeadingsOne()
        {
            return await _visitor.GetAllHeadingsOneAsync();
        }
        
        public async Task<IEnumerable<Entity.HeadingTwo>> GetAllHeadingsTwo()
        {
            return await _visitor.GetAllHeadingsTwoAsync();
        }
        
        public async Task<IEnumerable<Entity.HeadingTwo>> GetHeadingsTwoByHeadingsOne(string headingOneTitle)
        {
            return await _visitor.GetHeadingsTwoByHeadingsOneAsync(headingOneTitle);
        }
    }
}