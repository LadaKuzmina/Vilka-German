using System.Collections.Generic;
using System.Threading.Tasks;
using BackServer.Repositories;
using BackServer.RepositoryChangers.Interfaces;
using BackServer.Services.Interfaces;
using Entity;
using Microsoft.AspNetCore.Rewrite;

namespace BackServer.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly IPropertyVisitor _visitor;
        private readonly IPropertyChanger _changer;

        public PropertyService(IPropertyVisitor visitor, IPropertyChanger changer)
        {
            _visitor = visitor;
            _changer = changer;
        }

        public async Task<IEnumerable<string>> GetAllTitles()
        {
            return await _visitor.GetAllTitles();
        }

        public async Task<IEnumerable<Property>> GetAllByProduct(string productTitle)
        {
            return await _visitor.GetAllByProduct(productTitle);
        }

        public async Task<IEnumerable<Property>> GetPriorityByProduct(string productTitle)
        {
            return await _visitor.GetPriorityByProduct(productTitle);
        }

        public async Task<IEnumerable<Property>> GetByHeadingOne(string headingOneTitle)
        {
            return await _visitor.GetByHeadingOne(headingOneTitle);
        }

        public async Task<IEnumerable<Property>> GetByHeadingTwo(string headingTwoTitle)
        {
            return await _visitor.GetByHeadingTwo(headingTwoTitle);
        }

        public async Task<string> GetProductPropertyValue(string productTitle, string propertyTitle)
        {
            return await _visitor.GetProductPropertyValue(productTitle, propertyTitle);
        }

        public async Task<bool> AddProperty(Property property)
        {
            return await _changer.AddProperty(property);
        }

        public async Task<bool> AddPropertyValue(string propertyTitle, string[] propertyValues)
        {
            return await _changer.AddPropertyValue(propertyTitle, propertyValues);
        }

        public async Task<bool> AddProductPropertyValue(string productTitle, string propertyTitle, string propertyValue, bool isPriority)
        {
            return await _changer.AddProductPropertyValue(productTitle, propertyTitle, propertyValue,isPriority);
        }

        public Task<bool[]> AddProductProperties(string productTitle, Property[] properties)
        {
            return _changer.AddProductProperties(productTitle, properties);
        }

        public async Task<bool> DeleteProperty(string propertyTitle)
        {
            return await _changer.DeleteProperty(propertyTitle);
        }

        public async Task<bool> UpdateProperty(Property oldProperty, Property property)
        {
            return await _changer.UpdateProperty(oldProperty, property);
        }

        public async Task<bool> DeleteProductPropertyValue(string productTitle, string propertyTitle, string propertyValue)
        {
            return await _changer.DeleteProductPropertyValue(productTitle, propertyTitle, propertyValue);
        }

        public async Task<bool> UpdateProductPropertyValue(string productTitle, string propertyTitle, string oldPropertyValue,
            string newPropertyValue)
        {
            return await _changer.UpdateProductPropertyValue(productTitle, propertyTitle, oldPropertyValue,
                newPropertyValue);
        }

        public async Task<bool> DeleteAllProductProperties(string productTitle)
        {
            return await _changer.DeleteAllProductProperties(productTitle);
        }

        public async Task<bool> DeleteAllPropertyValues(string propertyTitle)
        {
            return await _changer.DeleteAllPropertyValues(propertyTitle);
        }

        public async Task<bool> DeletePropertyValue(string propertyTitle, string propertyValue)
        {
            return await _changer.DeletePropertyValue(propertyTitle, propertyValue);
        }

        public async Task<bool> AddFilterHeadingOne(string propertyTitle, string headingOneTitle)
        {
            return await _changer.AddFilterHeadingOne(propertyTitle, headingOneTitle);
        }

        public async Task<bool> DeleteHeadingOneFilter(string propertyTitle, string headingOneTitle)
        {
            return await _changer.DeleteHeadingOneFilter(propertyTitle, headingOneTitle);
        }

        public async Task<bool> DeleteAllHeadingOneFilters(string headingOneTitle)
        {
            return await _changer.DeleteAllHeadingOneFilters(headingOneTitle);
        }

        public async Task<bool> UpdateHeadingOneFilter(string headingOneFilter, string oldPropertyTitle, string newPropertyTitle)
        {
            return await _changer.UpdateHeadingOneFilter(headingOneFilter, oldPropertyTitle, newPropertyTitle);
        }

        public async Task<bool> AddFilterHeadingTwo(string propertyTitle, string headingTwoTitle)
        {
            return await _changer.AddFilterHeadingTwo(propertyTitle, headingTwoTitle);
        }

        public async Task<bool> DeleteHeadingTwoFilter(string propertyTitle, string headingTwoTitle)
        {
            return await _changer.DeleteHeadingTwoFilter(propertyTitle, headingTwoTitle);
        }

        public async Task<bool> DeleteAllHeadingTwoFilters(string headingTwoTitle)
        {
            return await _changer.DeleteAllHeadingTwoFilters(headingTwoTitle);
        }

        public async Task<bool> UpdateHeadingTwoFilter(string headingTwoFilter, string oldPropertyTitle, string newPropertyTitle)
        {
            return await _changer.UpdateHeadingTwoFilter(headingTwoFilter, oldPropertyTitle, newPropertyTitle);
        }
    }
}