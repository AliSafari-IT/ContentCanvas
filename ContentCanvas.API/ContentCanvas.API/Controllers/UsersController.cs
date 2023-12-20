using ContentCanvas.API.Data;
using ContentCanvas.API.Model;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace ContentCanvas.API.Controllers
{
    public class UsersController(MongoDbContext context) : Controller
    {
        private readonly MongoDbContext _context = context;
        public IActionResult Index()
        {
            return View();
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            return await _context.Users.Find(_ => true).ToListAsync();
        }


        // GET: api/Users/5
        [HttpGet("{id}")]
        public ActionResult<User> GetUser(string id)
        {
            var user = _context.Users.Find(user => user.Id == id).FirstOrDefault();

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // POST: api/Users
        [HttpPost]
        public ActionResult<User> PostUser(User user)
        {
            _context.Users.InsertOne(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }


        // PUT: api/Users/5
        [HttpPut("{id}")]
        public IActionResult PutUser(string id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            var result = _context.Users.ReplaceOne(u => u.Id == id, user);

            if (result.MatchedCount == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public ActionResult<User> DeleteUser(string id)
        {
            var result = _context.Users.DeleteOne(user => user.Id == id);

            if (result.DeletedCount == 0)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
