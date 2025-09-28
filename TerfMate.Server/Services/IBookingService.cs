using TurfMate.Server.Models.DTOs;

namespace TurfMate.Server.Services
{
    public interface IBookingService
    {
        Task<BookingDto> CreateBookingAsync(CreateBookingRequest createBookingDto, int userId);
        Task<List<BookingDto>> GetUserBookingsAsync(int userId);
        Task<List<BookingDto>> GetAllBookingsAsync();
        Task<BookingDto?> GetBookingByIdAsync(int id);
        Task<BookingDto?> UpdateBookingStatusAsync(int id, string status);
        Task<bool> CancelBookingAsync(int id, int userId);
        Task<bool> DeleteBookingAsync(int id);
        Task<List<TimeSlotResponse>> GetAvailableTimeSlotsAsync(TimeSlotRequest request);
    }
}