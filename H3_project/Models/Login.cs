using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace H3_project.Models
{
    public class Login
    {
        [Key]
        public int LoginID { get; set; } = 0;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int? UserTypeId { get; set; } // Foreign key property
        public UserType? userType { get; set; }  // Navigation property

        [JsonIgnore]
        //1-1 FK between Login and User.
        public User? User { get; set; }
    }
}
