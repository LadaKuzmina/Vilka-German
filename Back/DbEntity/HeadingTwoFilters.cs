namespace DbEntity
{
    public class HeadingTwoFilters
    {
        public int heading_two_id { get; set; }
        public int property_id { get; set; }
        public HeadingTwo HeadingTwo { get; set; }
        public Property Property { get; set; }
    }
}