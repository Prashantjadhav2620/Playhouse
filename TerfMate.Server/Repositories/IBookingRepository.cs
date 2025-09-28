using TerfMate.Server.Models;

namespace TurfMate.Server.Repositories
{
    public interface IBookingRepository
    {
        Task<Booking> CreateAsync(Booking booking);
        Task<List<BookingWithDetails>> GetByUserIdAsync(int userId);
        Task<List<BookingWithDetails>> GetAllAsync();
        Task<Booking?> GetByIdAsync(int id);
        Task<BookingWithDetails?> GetByIdWithDetailsAsync(int id);
        Task<Booking?> UpdateAsync(int id, Booking booking);
        Task<bool> DeleteAsync(int id);
        Task<bool> IsTimeSlotAvailableAsync(int turfId, DateTime bookingDate, TimeSpan startTime, TimeSpan endTime, int? excludeBookingId = null);
        Task<List<Booking>> GetBookingsByTurfAndDateAsync(int turfId, DateTime date);
    }
}