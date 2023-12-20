using ContentCanvas.API.Data;
using ContentCanvas.API.Model;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace ContentCanvas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly MongoDbContext _context;

        public PostsController(MongoDbContext context)
        {
            _context = context;
        }

        // GET: api/Posts
        [HttpGet]
        public ActionResult<IEnumerable<Post>> GetPosts()
        {
            return _context.Posts.Find(post => true).ToList();
        }

        // GET: api/Posts/5
        [HttpGet("{id}")]
        public ActionResult<Post> GetPost(string id)
        {
            var post = _context.Posts.Find(post => post.Id == id).FirstOrDefault();

            if (post == null)
            {
                return NotFound();
            }

            return post;
        }

        // POST: api/Posts
        [HttpPost]
        public ActionResult<Post> PostPost(Post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Optional: Validate UserId here if necessary

            _context.Posts.InsertOne(post);
            return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);
        }

        // PUT: api/Posts/5
        [HttpPut("{id}")]
        public IActionResult PutPost(string id, Post post)
        {
            if (id != post.Id)
            {
                return BadRequest();
            }

            var result = _context.Posts.ReplaceOne(p => p.Id == id, post);

            if (result.MatchedCount == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Posts/5
        [HttpDelete("{id}")]
        public IActionResult DeletePost(string id)
        {
            var result = _context.Posts.DeleteOne(post => post.Id == id);

            if (result.DeletedCount == 0)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
