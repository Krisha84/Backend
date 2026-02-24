using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BackendExam.Models
{
    public enum RoleType
    {
        Manager,
        Support,
        User
    }
    public class Roles
    {
        [Key]
        public int id { get; set; }

        public RoleType name { get; set; } 


        [JsonIgnore]
        public virtual ICollection<Users> Users { get; set; } = new List<Users>();
    }
}