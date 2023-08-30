using System.Collections.Generic;
using System.Threading.Tasks;
using Npgsql;

namespace BackServer.Repositories
{
    public interface IPropertyVisitor
    {
        Task<IEnumerable<string>> GetAllTitles();
        Task<IEnumerable<Entity.Property>> GetAllByProduct(string productTitle);
        Task<IEnumerable<Entity.Property>> GetPriorityByProduct(string productTitle);
        Task<IEnumerable<Entity.Property>> GetBySubstrings(string[] substrings);
        Task<IEnumerable<Entity.Property>> GetByHeadingOne(string headingOneTitle);
        Task<IEnumerable<Entity.Property>> GetByHeadingTwo(string headingTwoTitle);
        Task<IEnumerable<Entity.Property>> GetBySubstringsAtLeastOne(string[] substrings);
        Task<IEnumerable<Entity.Property>> GetByHeadingOneAtLeastOne(string headingOneTitle);
        Task<IEnumerable<Entity.Property>> GetByHeadingTwoAtLeastOne(string headingTwoTitle);
        Task<string> GetProductPropertyValue(string productTitle, string propertyTitle);
    }
}