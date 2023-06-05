using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DbEntity
{
    public class HeadingOne
    {
        public int Id { get; set; }
        public string Title { get; set; }

        [JsonIgnore]
        public List<HeadingTwo> HeadingsTwo { get; set; }
        public List<Product> Products { get; set; }
    }
}