using System;
using System.Collections.Generic;

namespace DbEntity
{
    public class HeadingOne : IHeading
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? PageLink { get; set; }
        public string? ImageRef { get; set; }
        public List<HeadingTwo> HeadingsTwo { get; set; }
        public List<ProductFamily> ProductFamilies { get; set; }
        public List<HeadingOneFilters> HeadingOneFilters { get; set; }
    }
}