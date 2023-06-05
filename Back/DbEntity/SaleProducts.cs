using Microsoft.EntityFrameworkCore;

namespace DbEntity
{
    [Keyless]
    public class SaleProducts
    {
        public int ProductId { get; set; }
        public int SaleId { get; set; }
        public Product Product { get; set; }
        public Sale Sale { get; set; }
    }
}