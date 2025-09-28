using TerfMate.Server.Models;

namespace TerfMate.Server.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TurfId { get; set; }
        public DateTime BookingDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "confirmed";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public User User { get; set; } = null!;
        public Turf Turf { get; set; } = null!;
    }

    public class BookingWithDetails
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

        // Turf details
        public string TurfName { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public decimal PricePerHour { get; set; }
        public string ImageUrl { get; set; } = string.Empty;

        // User details
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}