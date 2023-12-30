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
    public class PermissionsController : ControllerBase
    {
        private readonly MongoDbContext _context;

        public PermissionsController(MongoDbContext context)
        {
            _context = context;
        }

        // GET: api/Permissions
        [HttpGet]
        public async Task<ActionResult<List<Permission>>> GetPermissions()
        {
            return await _context.Permissions.Find(_ => true).ToListAsync();
        }

        // POST: api/Permissions
        [HttpPost]
        public async Task<ActionResult<Permission>> PostPermission(Permission permission)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _context.Permissions.InsertOneAsync(permission);
            return CreatedAtAction(nameof(GetPermission), new { id = permission.Id }, permission);
        }

        // GET: api/Permissions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Permission>> GetPermission(string id)
        {
            var permission = await _context.Permissions.Find(p => p.Id == id).FirstOrDefaultAsync();

            if (permission == null)
            {
                return NotFound();
            }

            return permission;
        }
    }
}
