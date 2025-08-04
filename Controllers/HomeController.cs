using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StugorAPI.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        [HttpGet("/dashboard")]
        public IActionResult Dashboard()
        {
            return Content($"Welcome {User.Identity.Name}! You are logged in.");
        }
    }
}
