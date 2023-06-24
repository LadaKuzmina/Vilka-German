using System.Collections.Generic;

namespace Entity
{
    public class Product
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int SalePrice { get; set; }
        public int Popularity { get; set; }
        public bool Available { get; set; }
        public int Quantity { get; set; }
        public string? PageLink { get; set; }
        public IEnumerable<string> ImageRefs { get; set; }
        public string HeadingOne { get; set; }
        public string HeadingTwo { get; set; }
        public string? HeadingThree { get; set; }
        public string ProductFamilyTitle { get; set; }
        public string UnitMeasurement { get; set; }

        public List<Property> Properties { get; set; }
        public IEnumerable<Property> PriorityProperties { get; set; }

        public Product(string title, string description, int price, int quantity, int popularity, bool available, string? pageLink, string unitMeasurement)
        {
            Title = title;
            Description = description;
            Price = price;
            Quantity = quantity;
            Popularity = popularity;
            Available = available;
            PageLink = pageLink;
            UnitMeasurement = unitMeasurement;
        }

        public Product(string title)
        {
            Title = title;
        }

        public Product()
        {
        }
    }
}