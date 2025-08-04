using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class OpenAIController : ControllerBase
{
    [HttpPost("description")]
    public IActionResult GenerateDescription([FromBody] dynamic request)
    {
        var title = request.title.ToString();
        var features = request.features.ToString();
        var description = $"En charmig stuga kallad '{title}' med {features}. Perfekt för en avkopplande semester.";
        return Ok(new { description });
    }
}
