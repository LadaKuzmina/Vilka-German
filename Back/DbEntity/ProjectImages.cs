namespace DbEntity
{
    public class ProjectImages
    {
        public int project_id { get; set; }
        public string ImageRef { get; set; }
        public bool IsPriority { get; set; }
        public Project Project { get; set; }
    }
}