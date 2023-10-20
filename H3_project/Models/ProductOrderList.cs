using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace H3_project.Models
{
    public class ProductOrderList
    {
        [Key]
        public int ProductOrderListID { get; set; } = 0;
        public int Quantity { get; set; } = 0;
        public int? ProductId { get; set; }
        public Product? Products { get; set; }
        public int? OrderId { get; set; }
        public Order? Orders { get; set; }

        
    }
}
