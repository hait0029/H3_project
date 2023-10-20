using System.Text.Json.Serialization;

namespace H3_project.Models
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; } = 0;
        public string CategoryName { get; set; } = string.Empty;
        public int Id { get; set; } = 0;


        public List<Product?> product { get; set; } = new List<Product?>(); // en til mange relation mellem Category til Product
    }
}
