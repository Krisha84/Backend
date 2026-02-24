using BackendExam.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BackendExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TicketAPIController : ControllerBase
    {

        #region Configuration
        private readonly BackendExamDbContext _context;

        public TicketAPIController(BackendExamDbContext context)
        {
            _context = context;
        }
        #endregion


        #region GetCurrentUser
        private (int userId, RoleType role) GetCurrentUser()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var roleClaim = User.FindFirst(ClaimTypes.Role)?.Value;

            if (userIdClaim == null || roleClaim == null)
                throw new UnauthorizedAccessException("Invalid Token");

            return (int.Parse(userIdClaim),
                    Enum.Parse<RoleType>(roleClaim));
        }
        #endregion


        #region Create Ticket
        [HttpPost("/tickets")]
        public async Task<IActionResult> PostTicket(Tickets ticket)
        {
            var currentUser = GetCurrentUser();

            ticket.created_by = currentUser.userId;
            ticket.created_at = DateTime.UtcNow;
            ticket.status = StatusType.Open;

            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            return Ok(ticket);
        }
        #endregion


        #region Get Tickets
        [HttpGet("/tickets")]
        public async Task<IActionResult> GetTickets()
        {
            var currentUser = GetCurrentUser();

            IQueryable<Tickets> tickets = _context.Tickets;

            if (currentUser.role == RoleType.User)
            {
                tickets = tickets.Where(t => t.created_by == currentUser.userId);
            }

            else if (currentUser.role == RoleType.Support)
            {
                tickets = tickets.Where(t => t.assigned_to == currentUser.userId);
            }

            return Ok(await tickets.ToListAsync());
        }
        #endregion


        #region Assign Ticket 
        [HttpPost("tickets/{id}/assign")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> AssignTicket(int id, [FromBody] int assignedUserId)
        {
            var ticket = await _context.Tickets.FindAsync(id);

            if (ticket == null)
                return NotFound("Ticket not found");

            ticket.assigned_to = assignedUserId;
            ticket.status = StatusType.In_Progress;

            await _context.SaveChangesAsync();

            return Ok("Ticket Assigned Successfully");
        }
        #endregion


        #region Update Ticket Status
        [HttpPost("tickets/{id}/status")]
        [Authorize(Roles = "Support,Manager")]
        public async Task<IActionResult> UpdateTicketStatus(int id, [FromBody] StatusType newStatus)
        {
            var ticket = await _context.Tickets.FindAsync(id);

            if (ticket == null)
                return NotFound("Ticket not found");

            ticket.status = newStatus;

            var log = new Ticket_status_logs
            {
                ticket_id = ticket.id,
                new_status = (NewStatus)newStatus,
                changed_at = DateTime.UtcNow
            };

            _context.TicketStatusLogs.Add(log);

            await _context.SaveChangesAsync();

            return Ok("Status Updated");
        }
        #endregion


        #region Delete Ticket
        [HttpDelete("tickets/{id}")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);

            if (ticket == null)
                return NotFound("Ticket not found");

            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();

            return Ok("Ticket Deleted Successfully");
        }
        #endregion

    }
}