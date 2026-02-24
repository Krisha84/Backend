using System.Text.Json.Serialization;

namespace BackendExam.Models
{

    public enum OldStatus
    {
        Open,
        In_Progress,
        Resolved,
        Closed
    }

    public enum NewStatus
    {
        Open,
        In_Progress,
        Resolved,
        Closed
    }

    public class Ticket_status_logs
    {

        public int id { get; set; }

        public int ticket_id { get; set; }

        public OldStatus old_status { get; set; }

        public NewStatus new_status { get; set; } 

        public int changed_by { get; set; }

        public DateTime changed_at { get; set; } = DateTime.Now;


        [JsonIgnore]
        public virtual Tickets? Tickets { get; set; } = null!;

        [JsonIgnore]
        public virtual Users? Users { get; set; } = null!;
    }
}