﻿using System.Collections.Generic;

namespace DbEntity
{
    public class Project
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string RoofType { get; set; }
        public string? PageLink { get; set; }
        public int Priority { get; set; }
        public List<ProjectMaterials> ProjectMaterials { get; set; }
        public List<Product> Products { get; set; }
        public List<ProjectImages> ProjectImages { get; set; }
        
        public Project(string title, string roofType)
        {
            Title = title;
            RoofType = roofType;
        }
    }
}