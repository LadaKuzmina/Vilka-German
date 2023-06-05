using Microsoft.EntityFrameworkCore;

namespace DbEntity
{
    [Keyless]
    public class ProjectMaterials
    {
        public int ProjectId { get; set; }
        public int ProductId { get; set; }
        public Project Project { get; set; }
        public Product Product { get; set; }
    }
}