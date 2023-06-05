using System.Collections.Generic;
using System.Dynamic;

namespace DbEntity
{
    public class Project
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string RoofType { get; set; }

        public List<ProjectMaterials> ProjectMaterials { get; set; }
    }
}