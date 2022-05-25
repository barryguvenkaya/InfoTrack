namespace InfoTrack.Controllers;

using InfoTrack.Abstract;
using InfoTrack.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class BookingController : ControllerBase
{
    private IBookingService _bookingService;

    public BookingController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpGet]
    public async Task<List<Booking>> Get() 
    {
        return await _bookingService.GetBookingsAsync();
    }

    [HttpGet("{bookingId}")]
    public async Task<ActionResult<Booking>> Get(Guid bookingId)
    {
        var booking = await _bookingService.GetBookingAsync(bookingId);

        if (booking is null)
        {
            return NotFound();
        }

        return booking;
    }

    /// <summary>
    /// Creates a Booking.
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /BookingPostRequest
    ///     {
    ///        "bookingTime": "09:30",
    ///        "name":"John Smith"
    ///     }
    ///
    /// </remarks>
    /// <param name="request"></param>
    /// <returns>A newly created request</returns>
    /// <response code="201">Returns the newly created request</response>
    /// <response code="400">If the request is invalid</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(Booking request)
    {
        await _bookingService.AddBookingAsync(request);
        return Ok(new { request.BookingId });
    }

    /// <summary>
    /// Deletes a specific Booking
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var item = await _bookingService.FindBookingAsync(id);
        if (item is null)
        {
            return NotFound();
        }
        await _bookingService.RemoveBookingAsync(item);
        return NoContent();
    }
}