namespace DbEntity
{
    public class ProductImages
    {
        public int product_id { get; set; }
        public string ImageRef { get; set; }
        public bool IsPriority { get; set; }
        public Product Product { get; set; }
    }
}