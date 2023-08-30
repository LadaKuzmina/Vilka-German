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
            ImageRefs = new List<string>();
            Properties = new List<Property>();
        }

        public Product(string title)
        {
            Title = title;
            ImageRefs = new List<string>();
            Properties = new List<Property>();
        }

        public Product()
        {
            ImageRefs = new List<string>();
            Properties = new List<Property>();
        }
    }
}