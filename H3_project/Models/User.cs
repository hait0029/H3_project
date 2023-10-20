using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace H3_project.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; } = 0;
        public string FName { get; set; } = string.Empty;
        public string LName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int Phone { get; set; } = 0;
        public int? LoginId { get; set; } // Foreign key property
        public Login? login { get; set; }// 1 til 1 relation aka Navigation property


        [JsonIgnore]
        public UserType? userType { get; set; }// Navigation property

        [JsonIgnore]
        public List<Order?> order { get; set; } = new List<Order?>(); // en til mange relation mellem Customers til Order

    }
}
