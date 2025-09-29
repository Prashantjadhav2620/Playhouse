
namespace TurfMate.Server.Models.DTOs
{
    public class BookingDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TurfId { get; set; }
        public DateTime BookingDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public TurfDto Turf { get; set; } = null!;
        public UserDto User { get; set; } = null!;
    }

    public class CreateBookingRequest
    {
        public int TurfId { get; set; }
        public DateTime BookingDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }

    public class UpdateBookingRequest
    {
        public string Status { get; set; } = string.Empty;
    }

    public class TimeSlotRequest
    {
        public int TurfId { get; set; }
        public DateTime Date { get; set; }
    }

    public class TimeSlotResponse
    {
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool IsAvailable { get; set; }
    }
}