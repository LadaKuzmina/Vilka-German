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
        Task<IEnumerable<Entity.Property>> GetByHeadingOne(string headingOneTitle);
        Task<IEnumerable<Entity.Property>> GetByHeadingTwo(string headingTwoTitle);
        Task<string> GetProductPropertyValue(string productTitle, string propertyTitle);
        
        
        Task<bool> AddProperty(Entity.Property property);
        Task<bool> AddPropertyValue(string propertyTitle, string[] propertyValues);
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


        Task<bool> AddFilterHeadingOne(string propertyTitle, string headingOneTitle);
        Task<bool> DeleteHeadingOneFilter(string propertyTitle, string headingOneTitle);
        Task<bool> DeleteAllHeadingOneFilters(string headingOneTitle);
        Task<bool> UpdateHeadingOneFilter(string headingOneFilter, string oldPropertyTitle,
            string newPropertyTitle);
        Task<bool> AddFilterHeadingTwo(string propertyTitle, string headingTwoTitle);
        Task<bool> DeleteHeadingTwoFilter(string propertyTitle, string headingTwoTitle);
        Task<bool> DeleteAllHeadingTwoFilters(string headingTwoTitle);
        Task<bool> UpdateHeadingTwoFilter(string headingTwoFilter, string oldPropertyTitle,
            string newPropertyTitle);
    }
}