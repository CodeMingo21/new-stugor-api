using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StugorSe_API.Models;

//[Authorize]
//[ApiController]
//[Route("api/[controller]")]
//public class CottageController : ControllerBase
//{
//    [HttpGet]
//    public IActionResult GetAllCottages()
//    {
//        // Example: Return hardcoded data
//        return Ok(new[] {
//            new Cottage { Id = 1, Title = "Skogsstuga i Dalarna", PricePerNight = 1200, Location = "Dalarna" },
//            new Cottage { Id = 2, Title = "Fjällstuga i Åre", PricePerNight = 1400, Location = "Åre" }
//        });
//    }

//    [HttpGet("{id}")]
//    public IActionResult GetCottage(int id)
//    {
//        return Ok(new Cottage { Id = id, Title = "Exempelstuga", PricePerNight = 1000 });
//    }
//}

[ApiController]
[Route("api/[controller]")]
public class CottagesController : ControllerBase
{
    private readonly IConfiguration _config;

    public CottagesController(IConfiguration config)
    {
        _config = config;
    }

    [HttpGet("search")]
    public IActionResult Search([FromQuery] string location)
    {
        var apiKey = Request.Headers["X-API-KEY"];
        var configuredKey = _config["ApiKey"];

        if (apiKey != configuredKey)
            return Unauthorized("Invalid API Key");

        // Continue with search logic
        return Ok(new { results = "stub" });
    }
}

