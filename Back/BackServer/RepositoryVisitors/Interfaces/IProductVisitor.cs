using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entity;
using NpgsqlDbExtensions.Enums;

namespace BackServer.Repositories
{
    public interface IProductVisitor
    {
        Task<IEnumerable<Product>> GetAll();
        Task<IEnumerable<Entity.Product>> GetAvailable();
        Task<Entity.Product?> GetByTitle(string title);
        Task<IEnumerable<Product>> GetBySubstring(string substring);
        Task<IEnumerable<Product>> GetBySubstrings(string[] substrings, ProductOrders productOrder,
            Dictionary<string, HashSet<string>> reqProperties, int pageNumber, int countElements);
        Task<IEnumerable<Entity.Product>> GetAllHeadingOne(string headingOneTitle);
        Task<IEnumerable<Entity.Product>> GetAllHeadingTwo(string headingTwoTitle);
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
    }
}