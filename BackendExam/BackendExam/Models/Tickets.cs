using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BackendExam.Models
{
    public enum StatusType
    {
        Open,
        In_Progress,
        Resolved,
        Closed
    }

    public enum PriorityType
    {
        Low,
        Medium,
        High
    }

    public class Tickets
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string title { get; set; } = null!;

        public string? description { get; set; }

        public StatusType status { get; set; }

        public PriorityType priority { get; set; }

        public int created_by { get; set; }

        public int? assigned_to { get; set; }

        public DateTime created_at { get; set; }

        [JsonIgnore]
        public virtual ICollection<Ticket_Comments> Ticket_Comments { get; set; } = new List<Ticket_Comments>();

        [JsonIgnore]
        public virtual ICollection<Ticket_status_logs> Ticket_Status_Logs { get; set; } = new List<Ticket_status_logs>();

        [JsonIgnore]
        public virtual Users? User { get; set; }
    }
}