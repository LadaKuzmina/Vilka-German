namespace DbEntity
{
    public class HeadingTwo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public HeadingOne HeadingOne { get; set; }
    }
}