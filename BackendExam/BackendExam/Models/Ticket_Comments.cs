using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BackendExam.Models
{
    public class Ticket_Comments
    {
        [Key]
        public int id { get; set; }

        public int ticket_id { get; set; }

        public int user_id { get; set; }

        public string comment { get; set; } = null!;

        public DateTime created_at { get; set; } = DateTime.Now;


        [JsonIgnore]
        public virtual Tickets? Tickets { get; set; } = null!;

        [JsonIgnore]
        public virtual Users? Users { get; set; } = null!;
    }
}
