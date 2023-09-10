namespace DbEntity
{
    public class HeadingTwoFilters : IHeadingFilter
    {
        public int heading_id { get; set; }
        public int property_values_id { get; set; }
        public int Count { get; set; }
        public HeadingTwo HeadingTwo { get; set; }
        public PropertyValues PropertyValues { get; set; }

    }
}