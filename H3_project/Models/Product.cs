using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace H3_project.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; } = 0;
        public string ProductName { get; set; } = string.Empty;
        public int Price { get; set; } = 0;
        public int? CategoryId { get; set; } // Foreign key property
        public Category? category { get; set; }  // Navigation property

        [JsonIgnore]
        public List<ProductOrderList?> orderlists { get; set; } = new List<ProductOrderList?>();
    }
}
