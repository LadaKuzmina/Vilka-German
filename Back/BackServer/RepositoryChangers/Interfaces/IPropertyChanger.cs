﻿using System.Threading.Tasks;
using Entity;

namespace BackServer.RepositoryChangers.Interfaces
{
    public interface IPropertyChanger
    {
        Task<bool> AddProperty(string propertyTitle);
        Task<bool> DeleteProperty(string propertyTitle);
        Task<bool> UpdateProperty(Property oldProperty, Property property);

        Task<bool> AddProductPropertyValue(string productTitle, string propertyTitle, string propertyValue,
            bool isPriority);

        Task<bool[]> AddProductProperties(string productTitle, Entity.Property[] properties);
        Task<bool> DeleteProductPropertyValue(string productTitle, string propertyTitle,
            string propertyValue);
        Task<bool> UpdateProductPropertyValue(string productTitle, string propertyTitle,
            string oldPropertyValue, string newPropertyValue);
        Task<bool> DeleteAllProductProperties(string productTitle);
        Task<bool> DeleteAllPropertyValues(string propertyTitle);
        Task<bool> DeletePropertyValue(string propertyTitle, string propertyValue);
        Task<bool> AddPropertyValue(string propertyTitle, IEnumerable<string> propertyValues);

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