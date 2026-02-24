using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BackendExam.Models
{
    public class Users
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string name { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        public string password { get; set; } = null!;

        [Required]
        public int role_id { get; set; }

        [Required]
        public DateTime created_at { get; set; } = DateTime.Now;


        [JsonIgnore]
        public virtual Roles? Roles { get; set; } = null!;

        [JsonIgnore]
        public virtual ICollection<Tickets> Tickets { get; set; } = new List<Tickets>();

        [JsonIgnore]
        public virtual ICollection<Ticket_Comments> Ticket_Comments { get; set; } = new List<Ticket_Comments>();

        [JsonIgnore]
        public virtual ICollection<Ticket_status_logs> Ticket_Status_Logs { get; set; } = new List<Ticket_status_logs>();

    }
}
