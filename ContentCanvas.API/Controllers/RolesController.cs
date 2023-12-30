using Microsoft.AspNetCore.Mvc;
using ContentCanvas.API.Model;
using ContentCanvas.API.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace ContentCanvas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly MongoDbContext _context;

        public RolesController(MongoDbContext context)
        {
            _context = context;
        }

        // GET: api/Role
        [HttpGet]
        public async Task<ActionResult<List<Role>>> GetRoles()
        {
            return await _context.Roles.Find(_ => true).ToListAsync();
        }

        // POST: api/Role
        [HttpPost]
        public async Task<ActionResult<Role>> PostRole(Role role)
        {
            await _context.Roles.InsertOneAsync(role);
            return CreatedAtAction("GetRole", new { id = role.Id }, role);
        }

        // Other actions like PUT, DELETE, etc.

    }
}
