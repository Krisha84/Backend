using BackendExam.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TicketCommentAPIController : ControllerBase
    {

        #region Configuration
        private readonly BackendExamDbContext _context;
        public TicketCommentAPIController(BackendExamDbContext context)
        {
            _context = context;
        }
        #endregion


        #region GetCommentsForTicket
        [HttpGet("/tickets/{ticketId}/comments")]

        public async Task<ActionResult<IEnumerable<Ticket_Comments>>> GetCommentsForTicket(int ticketId)
        {
            var comments = await _context.TicketComments
                .Where(c => c.ticket_id == ticketId)
                .OrderByDescending(c => c.created_at)
                .ToListAsync();

            if (comments == null || !comments.Any())
            {
                return NotFound($"No comments found for ticket with ID {ticketId}.");
            }

            return Ok(comments);
        }
        #endregion


        #region PostComments
        [HttpPost("/tickets/{ticketId}/comments")]
        public async Task<IActionResult> PostComments(int ticketId, [FromBody] Ticket_Comments newComment)
        {
            if (ticketId != newComment.ticket_id)
            {
                return BadRequest("Ticket ID in the route does not match the ticket ID in the body.");
            }

            newComment.created_at = DateTime.Now;

            _context.TicketComments.Add(newComment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetComment), new { id = newComment.id }, newComment);
        }
        #endregion


        #region GetComment
        [HttpGet("/comments/{id}", Name = "GetComment")]
        public async Task<ActionResult<Ticket_Comments>> GetComment(int id)
        {
            var comment = await _context.TicketComments.FindAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment);
        }
        #endregion


        #region UpdateComment
        [HttpPatch("/comments/{id}")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> PatchComment(int id, [FromBody] JsonPatchDocument<Ticket_Comments> patchDoc)
        {
            var comment = await _context.TicketComments.FindAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            patchDoc.ApplyTo(comment, (Microsoft.AspNetCore.JsonPatch.Adapters.IObjectAdapter)ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.TicketComments.Any(e => e.id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        #endregion


        #region DeleteComment
        [HttpDelete("/comments/{id}")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _context.TicketComments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            _context.TicketComments.Remove(comment);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion

    }
}
