using Microsoft.AspNetCore.Mvc;

namespace ContentCanvas.API.Controllers
{
    public class PostsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
