using System.Collections.Generic;
using System.Dynamic;

namespace  DbEntity
{
    public class Project
    {
        public int Id { get; }
        public string Title { get; }
        public string RoofType { get; }
        public IEnumerable<Product> MaterialsUsed { get; }
    }
}