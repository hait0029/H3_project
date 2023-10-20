using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace H3_project.Models
{
    public class UserType
    {
        [Key]
        public int UsertypeID { get; set; } = 0;

        public string UserNameType { get; set; } = string.Empty;
        public int Id { get; set; } = 0;

        [JsonIgnore]
        public List<Login?> logins { get; set; } = new List<Login?>();// en til mange relation mellem UserType til Login
    }
}
