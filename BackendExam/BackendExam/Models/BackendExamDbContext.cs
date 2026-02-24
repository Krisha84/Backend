using Microsoft.EntityFrameworkCore;
using BackendExam.Models;

namespace BackendExam.Models
{
    public class BackendExamDbContext : DbContext
    {
        public BackendExamDbContext(DbContextOptions<BackendExamDbContext> options) : base(options) { }

        public virtual DbSet<Roles> Roles { get; set; }

        public virtual DbSet<Users> Users { get; set; }
        
        public virtual DbSet<Tickets> Tickets { get; set; }
        
        public virtual DbSet<Ticket_Comments> TicketComments { get; set; }
        
        public virtual DbSet<Ticket_status_logs> TicketStatusLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 1. users.role_id → roles.id (many-to-one)
            modelBuilder.Entity<Users>()
                .HasOne(u => u.Roles)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.role_id);

            // 2. tickets.created_by → users.id (many-to-one)
            modelBuilder.Entity<Tickets>()
                .HasOne(t => t.User)
                .WithMany(u => u.Tickets)
                .HasForeignKey(t => t.created_by)
                .OnDelete(DeleteBehavior.Restrict); 

            // 3. tickets.assigned_to → users.id (many-to-one, nullable)
            modelBuilder.Entity<Tickets>()
                .HasOne(t => t.User)
                .WithMany(u => u.Tickets)
                .HasForeignKey(t => t.assigned_to)
                .IsRequired(false);

            // 4. ticket_comments.ticket_id → tickets.id (many-to-one)
            modelBuilder.Entity<Ticket_Comments>()
                .HasOne(tc => tc.Tickets)
                .WithMany(t => t.Ticket_Comments)
                .HasForeignKey(tc => tc.ticket_id);

            // 5. ticket_comments.user_id → users.id (many-to-one)
            modelBuilder.Entity<Ticket_Comments>()
                .HasOne(tc => tc.Users)
                .WithMany(u => u.Ticket_Comments)
                .HasForeignKey(tc => tc.user_id);

            // 6. ticket_status_logs.ticket_id → tickets.id (many-to-one)
            modelBuilder.Entity<Ticket_status_logs>()
                .HasOne(tsl => tsl.Tickets)
                .WithMany(t => t.Ticket_Status_Logs)
                .HasForeignKey(tsl => tsl.ticket_id);

            // 7. ticket_status_logs.changed_by → users.id (many-to-one)
            modelBuilder.Entity<Ticket_status_logs>()
                .HasOne(tsl => tsl.Users)
                .WithMany(u => u.Ticket_Status_Logs)
                .HasForeignKey(tsl => tsl.changed_by);
        }
    }
}