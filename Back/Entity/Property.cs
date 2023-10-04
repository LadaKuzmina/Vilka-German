using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Entity
{
    public class Property
    {
        public string Title { get; set; }
        public List<string> Values { get; set; }
        public bool IsPriority { get; set; }

        public Property(string title, IEnumerable<string> values)
        {
            Title = title;
            Values = values.ToList();
        }
        
        public Property()
        {
        }
    }
}