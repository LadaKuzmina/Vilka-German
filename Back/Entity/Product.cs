using System.Collections.Generic;

namespace Entity
{
    public class Product
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public HeadingOne HeadingOne { get; set; }
        public HeadingTwo HeadingTwo { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public HashSet<Property> Properties { get; set; }
    }
}