using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace H3_project.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; } = 0;
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public int? UserId { get; set; } // Foreign key property
        public User? user { get; set; }  // Navigation property

        [JsonIgnore]
        public List<ProductOrderList?> orderlists { get; set; } = new List<ProductOrderList?>();

    }
}
