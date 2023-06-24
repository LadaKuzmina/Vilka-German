using System.Collections.Generic;

namespace Entity
{
    public class Property
    {
        public string Title { get; set; }
        public IEnumerable<string> Values { get; set; }
        //public bool IsPriority { get; set; }

        public Property(string title, IEnumerable<string> values)
        {
            Title = title;
            Values = values;
        }
    }
}