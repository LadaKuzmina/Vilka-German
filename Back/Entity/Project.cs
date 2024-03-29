﻿using System.Collections.Generic;

namespace Entity
{
    public class Project
    {
        public string Title { get; set; }
        public string RoofType { get; set; }
        public string? PageLink { get; set; }
        public string[] ImageRefs { get; set; }

        public List<Product> Products { get; set; }

        public Project(string title, string roofType, string? pageLink)
        {
            Title = title;
            RoofType = roofType;
            PageLink = pageLink;
        }
        
        public Project(string title, string? pageLink)
        {
            Title = title;
            PageLink = pageLink;
        }
    }
}