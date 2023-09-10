﻿using System.Collections.Generic;

namespace DbEntity
{
    public class HeadingTwo : IHeading
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsVisible { get; set; }
        public string? PageLink { get; set; }
        public string? ImageRef { get; set; }
        public HeadingOne HeadingOne { get; set; }
        public List<ProductFamily> ProductFamilies { get; set; }
        public List<HeadingThree> HeadingsThree { get; set; }
        public List<HeadingTwoFilters> HeadingTwoFilters { get; set; }
    }
}