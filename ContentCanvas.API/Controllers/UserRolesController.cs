using Microsoft.AspNetCore.Mvc;
using ContentCanvas.API.Model;
using ContentCanvas.API.Data;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContentCanvas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRolesController : ControllerBase
    {
        private readonly MongoDbContext _context;

        public UserRolesController(MongoDbContext context)
        {
            _context = context;
        }

        // GET: api/UserRole
        [HttpGet]
        public async Task<ActionResult<List<UserRole>>> GetUserRoles()
        {
            return await _context.UserRoles.Find(_ => true).ToListAsync();
        }

        // GET: api/UserRole/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserRole>> GetUserRole(string id)
        {
            var userRole = await _context.UserRoles.Find(ur => ur.Id == id).FirstOrDefaultAsync();

            if (userRole == null)
            {
                return NotFound();
            }

            return userRole;
        }

        // POST: api/UserRole
        [HttpPost]
        public async Task<ActionResult<UserRole>> PostUserRole(UserRole userRole)
        {
            await _context.UserRoles.InsertOneAsync(userRole);
            return CreatedAtAction(nameof(GetUserRole), new { id = userRole.Id }, userRole);
        }

        // PUT: api/UserRole/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserRole(string id, UserRole userRole)
        {
            if (id != userRole.Id)
            {
                return BadRequest();
            }

            var result = await _context.UserRoles.ReplaceOneAsync(ur => ur.Id == id, userRole);

            if (result.MatchedCount == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/UserRole/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserRole(string id)
        {
            var result = await _context.UserRoles.DeleteOneAsync(ur => ur.Id == id);

            if (result.DeletedCount == 0)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
