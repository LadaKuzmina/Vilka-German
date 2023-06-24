using System.Collections.Generic;

namespace  DbEntity
{
    public class Property
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<PropertyValues> PropertyValues { get; set; }
        public List<HeadingOneFilters> HeadingOneFilters { get; set; }
        public List<HeadingTwoFilters> HeadingTwoFilters { get; set; }
    }
}