using BackendExam.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAPIController : ControllerBase
    {

        #region Configuration
        private readonly BackendExamDbContext _context;
        public UserAPIController(BackendExamDbContext context)
        {
            _context = context;
        }
        #endregion


        #region GetUsers
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _context.Users.ToListAsync());
        }
        #endregion


        #region PostUser
        [HttpPost]
        public async Task<IActionResult> PostUser(Users user)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsers", new { id = user.id }, user);

        }
        #endregion

    }
}
