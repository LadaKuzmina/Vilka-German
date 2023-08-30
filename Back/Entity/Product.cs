using System.Collections.Generic;

namespace Entity
{
    public class Product
    {
        public string Title { get; set; }
        public string Description { get; set; }

        private double price;

        public double Price
        {
            get => price;
            set => price = Math.Round(value, 2);
        }

        public int SalePrice { get; set; }
        public int Popularity { get; set; }
        public bool Available { get; set; }
        public int Quantity { get; set; }
        public string? PageLink { get; set; }
        public List<string> ImageRefs { get; set; }
        public string HeadingOne { get; set; }
        public string HeadingTwo { get; set; }
        public string? HeadingThree { get; set; }
        public string ProductFamilyTitle { get; set; }
        public string UnitMeasurement { get; set; }

        public List<Property> Properties { get; set; }
        public IEnumerable<Property> PriorityProperties { get; set; }

        private readonly int _hashValue;

        public Product(string title, string description, int price, int quantity, int popularity, bool available,
            string? pageLink, string unitMeasurement)
        {
            Title = title;
            Description = description;
            Price = price;
            Quantity = quantity;
            Popularity = popularity;
            Available = available;
            PageLink = pageLink;
            UnitMeasurement = unitMeasurement;
            _hashValue = GetFNVHashCode();
            ImageRefs = new List<string>();
            Properties = new List<Property>();
        }

        public Product(string title)
        {
            Title = title;
            _hashValue = GetFNVHashCode();
            ImageRefs = new List<string>();
            Properties = new List<Property>();
        }

        public Product()
        {
            _hashValue = GetFNVHashCode();
            ImageRefs = new List<string>();
            Properties = new List<Property>();
        }

        private int GetFNVHashCode()
        {
            unchecked
            {
                long p = 16777619;
                long hash = 2166136261;
                var propertiesValues = this.GetType().GetProperties()
                    .Where(property => property.Name != "_hashValue")
                    .Select(property => property.GetValue(this, null));
                
                foreach (var propertiesValue in propertiesValues)
                {
                    if (propertiesValue == null) continue;
                    hash ^= propertiesValue.GetHashCode();
                    hash *= p;
                }
                
                return (int)hash;
            }
        }
        
        public override int GetHashCode() => _hashValue;

        public override bool Equals(object? obj)
        {
            if (obj is not Product objProduct)
            {
                return false;
            }
            
            var thisPropertiesValues = GetType().GetProperties()
                .Where(property => property.Name != "_hashValue")
                .Select(property => property.GetValue(this, null)).ToList();
            var otherPropertiesValues = objProduct.GetType().GetProperties()
                .Where(property => property.Name != "_hashValue")
                .Select(property => property.GetValue(objProduct, null)).ToList();
            
            var areEqual = true;
            for (var i = 0; i < thisPropertiesValues.Count; i++)
            {
                if (thisPropertiesValues[i] == null && otherPropertiesValues[i] == null) continue;
                
                if ((thisPropertiesValues[i] == null && otherPropertiesValues[i] != null)
                    || (thisPropertiesValues[i] != null && otherPropertiesValues[i] == null)
                    || !thisPropertiesValues[i]!.Equals(otherPropertiesValues[i]))
                {
                    areEqual = false;
                    break;
                }
            }
            
            return areEqual;
        }
    }
}