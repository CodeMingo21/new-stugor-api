using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StugorAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : Controller
    {
        [HttpGet]
        public IActionResult GetUserInfo()
        {
            var claims = User.Claims.Select(c => new {
                c.Type,
                c.Value
            });

            return Ok(new
            {
                Message = "Authenticated user",
                Claims = claims
            });
        }
    }
}
