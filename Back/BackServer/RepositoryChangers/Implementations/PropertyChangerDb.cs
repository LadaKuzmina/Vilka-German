using System.Linq;
using System.Threading.Tasks;
using BackServer.Contexts;
using BackServer.RepositoryChangers.Interfaces;
using DbEntity;
using Microsoft.EntityFrameworkCore;
using Property = Entity.Property;

namespace BackServer.RepositoryChangers.Implementations
{
    public class PropertyChangerDb : IPropertyChanger
    {
        private readonly GsDbContext _context;
        private readonly string defaultPropertyValue = "Не указано";

        public PropertyChangerDb(GsDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddProperty(string propertyTitle)
        {
            if (!await _context.Properties.AnyAsync(x => x.Title == propertyTitle))
            {
                var propertyDb = new DbEntity.Property() {Title = propertyTitle};
                await _context.Properties.AddAsync(propertyDb);
                await _context.SaveChangesAsync();
            }

            return true;
        }
        
        public async Task<bool> AddPropertyValue(string propertyTitle, IEnumerable<string> propertyValues)
        {
            var property = await _context.Properties.FirstOrDefaultAsync(x => x.Title == propertyTitle);
            if (property == null)
            {
                await AddProperty(propertyTitle);
                property = await _context.Properties.FirstAsync(x => x.Title == propertyTitle);
            }
            
            foreach (var value in propertyValues)
            {
                if (!await _context.PropertyValues.AnyAsync(x =>
                    x.Property==property && x.PropertyValue == value))
                {
                    var propertyValue = new DbEntity.PropertyValues()
                        {Property = property, PropertyValue = value};
                    await _context.PropertyValues.AddAsync(propertyValue);
                    await _context.SaveChangesAsync();
                }
            }


            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteProperty(string propertyTitle)
        {
            var property = await _context.Properties.FirstOrDefaultAsync(x => x.Title == propertyTitle);
            if (property == null)
                return false;

            var propertyValuesSet = _context.PropertyValues
                .Where(x => x.Property == property)
                .ToHashSet();
            foreach (var propertyValue in propertyValuesSet)
            {
                await DeletePropertyValue(propertyTitle, propertyValue.PropertyValue);
            }
            
            var headingsOneFilters = await _context.HeadingOneFilters
                .Where(x => x.property_values_id == property.Id)
                .ToArrayAsync();
            var headingsTwoFilters = await _context.HeadingTwoFilters
                .Where(x => x.property_values_id == property.Id)
                .ToArrayAsync();

            
            _context.PropertyValues.RemoveRange(propertyValuesSet);
            _context.HeadingOneFilters.RemoveRange(headingsOneFilters);
            _context.HeadingTwoFilters.RemoveRange(headingsTwoFilters);

            _context.Properties.Remove(property);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePropertyValue(string propertyTitle, string propertyValue)
        {
            var property = await _context.Properties.FirstOrDefaultAsync(x => x.Title == propertyTitle);
            if (property == null)
                return false;

            var propertyValueDb = await _context.PropertyValues
                .FirstOrDefaultAsync(x => x.Property == property && x.PropertyValue == propertyValue);
            if (propertyValueDb == null)
                return false;

            var headingsThree = await _context.HeadingsThree
                .Where(x => x.PropertyValues==propertyValueDb)
                .ToArrayAsync();
            await SetPropertyDefault(propertyTitle, propertyValue);
            
            _context.HeadingsThree.RemoveRange(headingsThree);

            _context.PropertyValues.Remove(propertyValueDb);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateProperty(Property oldProperty, Property property)
        {
            var propertyDb = await _context.Properties.FirstOrDefaultAsync(x => x.Title == oldProperty.Title);
            if (propertyDb == null)
                return false;

            propertyDb.Title = property.Title;
            if (oldProperty.Values != property.Values)
            {
                var propertyValues = await _context.PropertyValues.FirstAsync(
                    x => x.Property.Id == propertyDb.Id && x.PropertyValue == oldProperty.Values.First());
                propertyValues.PropertyValue = property.Values.First();
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool[]> AddProductProperties(string productTitle, Entity.Property[] properties)
        {
            var res = new bool[properties.Length];
            for (var i = 0; i < properties.Length; i++)
            {
                var property = properties[i];
                res[i] = await AddProductPropertyValue(productTitle, property.Title, property.Values.First(),
                    property.IsPriority);
            }

            return res;
        }

        public async Task<bool> AddProductPropertyValue(string productTitle, string propertyTitle, string propertyValue,
            bool isPriority)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Title == productTitle);
            if (product == null)
                return false;
            
            
            var property = await _context.Properties.FirstOrDefaultAsync(x => x.Title == propertyTitle);
            if (property == null)
            {
                await AddProperty(propertyTitle);
                property = await _context.Properties.FirstAsync(x => x.Title == propertyTitle);
            }

            var productProperty = await _context.ProductProperties.FirstOrDefaultAsync(x =>
                x.Product == product && x.PropertyValues.Property == property);
            if (productProperty != null)
            {
                return true;
            }

            var propertyValues =
                await _context.PropertyValues.FirstOrDefaultAsync(x =>
                    x.Property.Id == property.Id && x.PropertyValue == propertyValue);

            if (propertyValues == null)
            {
                propertyValues = new DbEntity.PropertyValues()
                    {Property = property, PropertyValue = propertyValue};

                await _context.PropertyValues.AddAsync(propertyValues);
                await _context.SaveChangesAsync();
            }

            propertyValues =
                await _context.PropertyValues.FirstAsync(x =>
                    x.Property.Id == property.Id && x.PropertyValue == propertyValue);

            await _context.ProductProperties.AddAsync(new ProductProperty()
            {
                PropertyValues = propertyValues, Product = product, IsPriority = isPriority
            });

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteProductPropertyValue(string productTitle, string propertyTitle,
            string propertyValue)
        {
            var productProperty = await _context.ProductProperties
                .FirstOrDefaultAsync(x =>
                    x.Product.Title == productTitle && x.PropertyValues.Property.Title == propertyTitle &&
                    x.PropertyValues.PropertyValue == propertyValue);
            if (productProperty == null)
                return false;
            _context.ProductProperties.Remove(productProperty);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateProductPropertyValue(string productTitle, string propertyTitle,
            string oldPropertyValue, string newPropertyValue)
        {
            var productProperty = await _context.ProductProperties
                .FirstOrDefaultAsync(x =>
                    x.Product.Title == productTitle && x.PropertyValues.Property.Title == propertyTitle &&
                    x.PropertyValues.PropertyValue == oldPropertyValue);
            if (productProperty == null)
                return false;

            var p = await _context.PropertyValues.FindAsync(productProperty.property_values_id);
            p.PropertyValue = newPropertyValue;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAllProductProperties(string productTitle)
        {
            var productProperties = await _context.ProductProperties.Where(x =>
                x.Product.Title == productTitle).ToArrayAsync();
            if (productProperties.Length == 0)
                return false;
            _context.ProductProperties.RemoveRange(productProperties);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAllPropertyValues(string propertyTitle)
        {
            var propertyValues = _context.PropertyValues.Where(x => x.Property.Title == propertyTitle).ToHashSet();
            if (propertyValues.Count == 0)
                return false;

            var productProperties = await _context.ProductProperties
                .Where(x => propertyValues.Contains(x.PropertyValues))
                .ToArrayAsync();

            _context.ProductProperties.RemoveRange(productProperties);
            _context.PropertyValues.RemoveRange(propertyValues);

            await _context.SaveChangesAsync();
            return true;
        }

        private async Task SetPropertyDefault(string propertyTitle, string propertyValue)
        {
            var defaultPV = await _context.PropertyValues
                .FirstOrDefaultAsync(x => x.Property.Title == propertyTitle && x.PropertyValue == defaultPropertyValue);
            if (defaultPV == null)
            {
                await AddPropertyValue(propertyTitle, new[] {defaultPropertyValue});
            }
            
            defaultPV = await _context.PropertyValues
                .FirstAsync(x => x.Property.Title == propertyTitle && x.PropertyValue == defaultPropertyValue);

            var productProperties = await _context.ProductProperties
                .Where(x => x.PropertyValues.PropertyValue == propertyValue)
                .ToArrayAsync();
            foreach (var productProperty in productProperties)
            {
                productProperty.PropertyValues = defaultPV;
            }

            await _context.SaveChangesAsync();
        }


    }
}