namespace  DbEntity
{
    public class Product
    {
        public int Id { get; }
        public string Title { get; }
        public string Description { get; }
        public HeadingOne HeadingOne { get; }
        public HeadingTwo HeadingTwo { get; }
        public int Price { get; }
        public int Quantity { get; }
    }
}