using System.Collections.Generic;
using System.Threading.Tasks;
using Entity;

namespace BackServer.Services.Interfaces
{
    public interface IPropertyService
    {
        Task<IEnumerable<string>> GetAllTitles();
        Task<IEnumerable<Entity.Property>> GetAllByProduct(string productTitle);
        Task<IEnumerable<Entity.Property>> GetPriorityByProduct(string productTitle);
        Task<string> GetProductPropertyValue(string productTitle, string propertyTitle);
        
        
        Task<bool> AddProperty(string propertyTitle);
        Task<bool> AddPropertyValue(string propertyTitle, IEnumerable<string> propertyValues);
        Task<bool> AddProductPropertyValue(string productTitle, string propertyTitle, string propertyValue,
            bool isPriority);

        Task<bool[]> AddProductProperties(string productTitle, Entity.Property[] properties);
        Task<bool> DeleteProperty(string propertyTitle);
        Task<bool> UpdateProperty(Property oldProperty, Property property);
        Task<bool> DeleteProductPropertyValue(string productTitle, string propertyTitle,
            string propertyValue);
        Task<bool> UpdateProductPropertyValue(string productTitle, string propertyTitle,
            string oldPropertyValue, string newPropertyValue);
        Task<bool> DeleteAllProductProperties(string productTitle);
        Task<bool> DeleteAllPropertyValues(string propertyTitle);
        Task<bool> DeletePropertyValue(string propertyTitle, string propertyValue);
    }
}