using System.Collections.Generic;
using System.Threading.Tasks;
using Entity;
using NpgsqlDbExtensions.Enums;

namespace BackServer.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAll();
        Task<IEnumerable<Entity.Product>> GetAvailable();
        Task<Entity.Product?> GetByTitle(string title);
        Task<IEnumerable<Entity.Product>> GetBySubstring(string substring);
        Task<IEnumerable<Entity.Product>> GetBySubstrings(string[] substrings);
        Task<IEnumerable<Entity.Product>> GetPageHeadingOne(string headingOneTitle, ProductOrders productOrder,
            Dictionary<string, HashSet<string>> reqProperties, int pageNumber, int countElements);
        Task<IEnumerable<Entity.Product>> GetPageHeadingTwo(string headingTwoTitle, ProductOrders productOrder,
            Dictionary<string, HashSet<string>> reqProperties, int pageNumber, int countElements);
        Task<IEnumerable<Product>> GetPageHeadingThree(string headingThreeTitle, ProductOrders productOrder,
            Dictionary<string, HashSet<string>> reqProperties, int pageNumber, int countElements);
        Task<int> GetCountPagesHeadingOne(string headingOneTitle, ProductOrders productOrder,
            Dictionary<string, HashSet<string>> reqProperties, int countElements);
        Task<int> GetCountPagesHeadingTwo(string headingTwoTitle, ProductOrders productOrder,
            Dictionary<string, HashSet<string>> reqProperties, int countElements);

        
        
        Task<bool> Add(Product product);
        Task<bool> Delete(HashSet<string> productTitles);
        Task<bool> Update(string oldProductTitle, Product product);
        Task<bool> UpdatePopularity(string productTitle, int newPopularity);
        Task<bool> DeleteHeadingOneProducts(string headingOneTitle);
        Task<bool> DeleteHeadingTwoProducts(string headingTwoTitle);
        Task<bool> DeleteHeadingThreeProducts(string headingThreeTitle);

    }
}