namespace DbEntity
{
    public class HeadingOneFilters
    {
        public int heading_one_id { get; set; }
        public int property_id { get; set; }
        public HeadingOne HeadingOne { get; set; }
        public Property Property { get; set; }
    }
}