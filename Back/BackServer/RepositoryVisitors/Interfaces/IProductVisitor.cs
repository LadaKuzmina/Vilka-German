using System.Collections.Generic;
using System.Threading.Tasks;
using Entity;

namespace BackServer.Repositories
{
    public interface IProductVisitor
    {
        public Task<IEnumerable<Product>> GetAll();
        public Task<IEnumerable<Entity.Product>> GetByHeadingOne(HeadingOne heading);
        public Task<IEnumerable<Entity.Product>> GetByHeadingTwo(HeadingTwo heading);
    }
}