using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TurfMate.Server.Models.DTOs;
using TurfMate.Server.Services;

namespace TerfMate.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly ILogger<BookingController> _logger;

        public BookingController(IBookingService bookingService, ILogger<BookingController> logger)
        {
            _bookingService = bookingService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<BookingDto>>> GetUserBookings()
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                var bookings = await _bookingService.GetUserBookingsAsync(userId);
                return Ok(bookings);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting user bookings");
                return StatusCode(500, new { message = "An error occurred while retrieving bookings" });
            }
        }

        [HttpGet("all")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<List<BookingDto>>> GetAllBookings()
        {
            try
            {
                var bookings = await _bookingService.GetAllBookingsAsync();
                return Ok(bookings);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all bookings");
                return StatusCode(500, new { message = "An error occurred while retrieving bookings" });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookingDto>> GetBookingById(int id)
        {
            try
            {
                var booking = await _bookingService.GetBookingByIdAsync(id);
                if (booking == null)
                {
                    return NotFound(new { message = "Booking not found" });
                }

                // Users can only see their own bookings unless they're admin
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                var userRole = User.FindFirst(ClaimTypes.Role)!.Value;

                if (userRole != "admin" && booking.UserId != userId)
                {
                    return Forbid();
                }

                return Ok(booking);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting booking with ID: {BookingId}", id);
                return StatusCode(500, new { message = "An error occurred while retrieving the booking" });
            }
        }

        [HttpPost]
        public async Task<ActionResult<BookingDto>> CreateBooking(CreateBookingRequest createBookingDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                var booking = await _bookingService.CreateBookingAsync(createBookingDto, userId);
                return CreatedAtAction(nameof(GetBookingById), new { id = booking.Id }, booking);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Invalid booking request");
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Booking creation failed");
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating booking");
                return StatusCode(500, new { message = "An error occurred while creating the booking" });
            }
        }

        [HttpPut("{id}/status")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<BookingDto>> UpdateBookingStatus(int id, UpdateBookingRequest updateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var booking = await _bookingService.UpdateBookingStatusAsync(id, updateDto.Status);
                if (booking == null)
                {
                    return NotFound(new { message = "Booking not found" });
                }
                return Ok(booking);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Invalid status update for booking ID: {BookingId}", id);
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating booking status for ID: {BookingId}", id);
                return StatusCode(500, new { message = "An error occurred while updating the booking status" });
            }
        }

        [HttpPut("{id}/cancel")]
        public async Task<ActionResult> CancelBooking(int id)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                var result = await _bookingService.CancelBookingAsync(id, userId);
                if (!result)
                {
                    return NotFound(new { message = "Booking not found or you don't have permission to cancel this booking" });
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error canceling booking with ID: {BookingId}", id);
                return StatusCode(500, new { message = "An error occurred while canceling the booking" });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> DeleteBooking(int id)
        {
            try
            {
                var result = await _bookingService.DeleteBookingAsync(id);
                if (!result)
                {
                    return NotFound(new { message = "Booking not found" });
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting booking with ID: {BookingId}", id);
                return StatusCode(500, new { message = "An error occurred while deleting the booking" });
            }
        }

        [HttpPost("timeslots")]
        public async Task<ActionResult<List<TimeSlotResponse>>> GetAvailableTimeSlots(TimeSlotRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var timeSlots = await _bookingService.GetAvailableTimeSlotsAsync(request);
                return Ok(timeSlots);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting time slots for turf ID: {TurfId} and date: {Date}", request.TurfId, request.Date);
                return StatusCode(500, new { message = "An error occurred while retrieving time slots" });
            }
        }
    }
}