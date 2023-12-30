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
    public class RolePermissionsController : ControllerBase
    {
        private readonly MongoDbContext _context;

        public RolePermissionsController(MongoDbContext context)
        {
            _context = context;
        }

        // GET: api/RolePermission
        [HttpGet]
        public async Task<ActionResult<List<RolePermission>>> GetRolePermissions()
        {
            return await _context.RolePermissions.Find(_ => true).ToListAsync();
        }

        // GET: api/RolePermission/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RolePermission>> GetRolePermission(string id)
        {
            var rolePermission = await _context.RolePermissions.Find(rp => rp.Id == id).FirstOrDefaultAsync();

            if (rolePermission == null)
            {
                return NotFound();
            }

            return rolePermission;
        }

        // POST: api/RolePermission
        [HttpPost]
        public async Task<ActionResult<RolePermission>> PostRolePermission(RolePermission rolePermission)
        {
            await _context.RolePermissions.InsertOneAsync(rolePermission);
            return CreatedAtAction(nameof(GetRolePermission), new { id = rolePermission.Id }, rolePermission);
        }

        // PUT: api/RolePermission/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRolePermission(string id, RolePermission rolePermission)
        {
            if (id != rolePermission.Id)
            {
                return BadRequest();
            }

            var result = await _context.RolePermissions.ReplaceOneAsync(rp => rp.Id == id, rolePermission);

            if (result.MatchedCount == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/RolePermission/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRolePermission(string id)
        {
            var result = await _context.RolePermissions.DeleteOneAsync(rp => rp.Id == id);

            if (result.DeletedCount == 0)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
