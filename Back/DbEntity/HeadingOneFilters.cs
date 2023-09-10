namespace DbEntity
{
    public class HeadingOneFilters : IHeadingFilter
    {
        public int heading_id { get; set; }
        public int property_values_id { get; set; }
        public int Count { get; set; }
        public HeadingOne HeadingOne { get; set; }
        public PropertyValues PropertyValues { get; set; }
    }
}