using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StugorSe_API.Models;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class BookingController : ControllerBase
{
    [HttpPost]
    public IActionResult CreateBooking([FromBody] Booking booking)
    {
        // Simulate booking confirmation
        return Ok(new { message = "Booking confirmed", booking });
    }
}
