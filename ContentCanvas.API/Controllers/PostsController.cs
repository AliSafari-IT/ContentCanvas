using ContentCanvas.API.Data;
using ContentCanvas.API.Model;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ContentCanvas.API.Controllers
{
    [ApiController]
    [Route("[controller]")]


    public class PostsController(MongoDbContext context) : Controller
    {
        private readonly MongoDbContext _context = context;

        // GET: api/Posts
        [HttpGet]
        public ActionResult<IEnumerable<Post>> GetPosts()
        {
            return _context.Posts.Find(post => post.IdObject != null).ToList();
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
            // If Id is provided, validate if it's in the correct format
            if (!string.IsNullOrEmpty(post.IdObject) && !ObjectId.TryParse(post.IdObject, out _))
            {
                return BadRequest("Invalid ObjectId format.");
            }

            _context.Posts.InsertOne(post);
            return CreatedAtAction(nameof(GetPost), new { id = post.IdObject }, post);
        }


        // PUT: api/Posts/5
        [HttpPut("{id}")]
        public IActionResult PutPost(string id, Post post)
        {
            if (id != post.IdObject)
            {
                return BadRequest();
            }

            var result = _context.Posts.ReplaceOne(p => p.IdObject == id, post);

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
            var result = _context.Posts.DeleteOne(post => post.IdObject == id);

            if (result.DeletedCount == 0)
            {
                return NotFound();
            }

            return NoContent();
        }


    }
}
