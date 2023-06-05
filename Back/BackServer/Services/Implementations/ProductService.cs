using System.Collections.Generic;
using System.Threading.Tasks;
using BackServer.Repositories;
using BackServer.Services.Interfaces;
using Entity;

namespace BackServer.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductVisitor _visitor;

        public ProductService(IProductVisitor visitor)
        {
            _visitor = visitor;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _visitor.GetAll();
        }

        public async Task<IEnumerable<Product>> GetByHeadingOne(HeadingOne heading)
        {
            return await _visitor.GetByHeadingOne(heading);
        }

        public async Task<IEnumerable<Product>> GetByHeadingTwo(HeadingTwo heading)
        {
            return await _visitor.GetByHeadingTwo(heading);
        }
    }
}