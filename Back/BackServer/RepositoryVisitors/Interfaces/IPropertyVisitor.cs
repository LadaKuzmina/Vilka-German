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
        Task<string?> GetProductPropertyValue(string productTitle, string propertyTitle);
    }
}