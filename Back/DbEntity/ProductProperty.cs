﻿using Microsoft.EntityFrameworkCore;

namespace  DbEntity
{
    [Keyless]
    public class ProductProperty
    {
        public int ProductId { get; set; }
        public int PropertyId { get; set; }
        public Product Product{ get; set; }
        public Property Property { get; set;}
        public string? PropertyValue { get; set;}
    }
}