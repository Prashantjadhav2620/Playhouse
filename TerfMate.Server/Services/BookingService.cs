using TerfMate.Server.Models;
using TurfMate.Server.Models.DTOs;
using TurfMate.Server.Repositories;
using TurfMate.Server.Services;

namespace TerfMate.Server.Services
{
        public class BookingService : IBookingService
        {
            private readonly IBookingRepository _bookingRepository;
            private readonly ITurfRepository _turfRepository;
            private readonly IUserRepository _userRepository;

            public BookingService(IBookingRepository bookingRepository, ITurfRepository turfRepository, IUserRepository userRepository)
            {
                _bookingRepository = bookingRepository;
                _turfRepository = turfRepository;
                _userRepository = userRepository;
            }

            public async Task<BookingDto> CreateBookingAsync(CreateBookingRequest createBookingDto, int userId)
            {
                // Validate turf exists
                var turf = await _turfRepository.GetByIdAsync(createBookingDto.TurfId);
                if (turf == null)
                {
                    throw new ArgumentException("Turf not found");
                }

                if (!turf.IsAvailable)
                {
                    throw new InvalidOperationException("Turf is not available for booking");
                }

                // Validate user exists
                var user = await _userRepository.GetByIdAsync(userId);
                if (user == null)
                {
                    throw new ArgumentException("User not found");
                }

                // Validate time slot
                if (createBookingDto.StartTime >= createBookingDto.EndTime)
                {
                    throw new ArgumentException("End time must be after start time");
                }

                if (createBookingDto.BookingDate < DateTime.Today)
                {
                    throw new ArgumentException("Cannot book for past dates");
                }

                // Validate time slot availability
                var isAvailable = await _bookingRepository.IsTimeSlotAvailableAsync(
                    createBookingDto.TurfId,
                    createBookingDto.BookingDate,
                    createBookingDto.StartTime,
                    createBookingDto.EndTime);

                if (!isAvailable)
                {
                    throw new InvalidOperationException("The selected time slot is not available");
                }

                // Calculate total amount
                var duration = createBookingDto.EndTime - createBookingDto.StartTime;
                var totalAmount = turf.PricePerHour * (decimal)duration.TotalHours;

                var booking = new Booking
                {
                    UserId = userId,
                    TurfId = createBookingDto.TurfId,
                    BookingDate = createBookingDto.BookingDate.Date,
                    StartTime = createBookingDto.StartTime,
                    EndTime = createBookingDto.EndTime,
                    TotalAmount = totalAmount,
                    Status = "confirmed",
                    CreatedAt = DateTime.UtcNow
                };

                var createdBooking = await _bookingRepository.CreateAsync(booking);
                return await MapToDto(createdBooking);
            }

            public async Task<List<BookingDto>> GetUserBookingsAsync(int userId)
            {
                var bookings = await _bookingRepository.GetByUserIdAsync(userId);
                var bookingDtos = new List<BookingDto>();

                foreach (var booking in bookings)
                {
                    bookingDtos.Add(await MapToDto(booking));
                }

                return bookingDtos;
            }

            public async Task<List<BookingDto>> GetAllBookingsAsync()
            {
                var bookings = await _bookingRepository.GetAllAsync();
                var bookingDtos = new List<BookingDto>();

                foreach (var booking in bookings)
                {
                    bookingDtos.Add(await MapToDto(booking));
                }

                return bookingDtos;
            }

            public async Task<BookingDto?> GetBookingByIdAsync(int id)
            {
                var booking = await _bookingRepository.GetByIdWithDetailsAsync(id);
                return booking == null ? null : await MapToDto(booking);
            }

            public async Task<BookingDto?> UpdateBookingStatusAsync(int id, string status)
            {
                var validStatuses = new[] { "confirmed", "cancelled", "completed" };
                if (!validStatuses.Contains(status.ToLower()))
                {
                    throw new ArgumentException("Invalid status");
                }

                var existingBooking = await _bookingRepository.GetByIdAsync(id);
                if (existingBooking == null) return null;

                var updatedBooking = new Booking
                {
                    Status = status.ToLower()
                };

                var result = await _bookingRepository.UpdateAsync(id, updatedBooking);
                return result == null ? null : await MapToDto(result);
            }

            public async Task<bool> CancelBookingAsync(int id, int userId)
            {
                var booking = await _bookingRepository.GetByIdAsync(id);
                if (booking == null || booking.UserId != userId)
                {
                    return false;
                }

                var updatedBooking = new Booking
                {
                    Status = "cancelled"
                };

                var result = await _bookingRepository.UpdateAsync(id, updatedBooking);
                return result != null;
            }

            public async Task<bool> DeleteBookingAsync(int id)
            {
                return await _bookingRepository.DeleteAsync(id);
            }

            public async Task<List<TimeSlotResponse>> GetAvailableTimeSlotsAsync(TimeSlotRequest request)
            {
                var existingBookings = await _bookingRepository.GetBookingsByTurfAndDateAsync(request.TurfId, request.Date);
                var timeSlots = GenerateTimeSlots();

                var availableSlots = timeSlots.Select(slot => new TimeSlotResponse
                {
                    StartTime = slot.StartTime,
                    EndTime = slot.EndTime,
                    IsAvailable = !existingBookings.Any(booking =>
                        booking.Status != "cancelled" &&
                        booking.StartTime < slot.EndTime &&
                        booking.EndTime > slot.StartTime)
                }).ToList();

                return availableSlots;
            }

            private async Task<BookingDto> MapToDto(Booking booking)
            {
                var turf = await _turfRepository.GetByIdAsync(booking.TurfId);
                var user = await _userRepository.GetByIdAsync(booking.UserId);

                return new BookingDto
                {
                    Id = booking.Id,
                    UserId = booking.UserId,
                    TurfId = booking.TurfId,
                    BookingDate = booking.BookingDate,
                    StartTime = booking.StartTime,
                    EndTime = booking.EndTime,
                    TotalAmount = booking.TotalAmount,
                    Status = booking.Status,
                    CreatedAt = booking.CreatedAt,
                    Turf = new TurfDto
                    {
                        Id = turf!.Id,
                        Name = turf.Name,
                        Location = turf.Location,
                        Description = turf.Description,
                        PricePerHour = turf.PricePerHour,
                        ImageUrl = turf.ImageUrl,
                        IsAvailable = turf.IsAvailable
                    },
                    User = new UserDto
                    {
                        Id = user!.Id,
                        Email = user.Email,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Role = user.Role,
                        CreatedAt = user.CreatedAt
                    }
                };
            }

            private async Task<BookingDto> MapToDto(BookingWithDetails booking)
            {
                var user = await _userRepository.GetByIdAsync(booking.UserId);

                return new BookingDto
                {
                    Id = booking.Id,
                    UserId = booking.UserId,
                    TurfId = booking.TurfId,
                    BookingDate = booking.BookingDate,
                    StartTime = booking.StartTime,
                    EndTime = booking.EndTime,
                    TotalAmount = booking.TotalAmount,
                    Status = booking.Status,
                    CreatedAt = booking.CreatedAt,
                    Turf = new TurfDto
                    {
                        Id = booking.TurfId,
                        Name = booking.TurfName,
                        Location = booking.Location,
                        Description = "", // You might want to include this in the stored procedure
                        PricePerHour = booking.PricePerHour,
                        ImageUrl = booking.ImageUrl,
                        IsAvailable = true // You might want to include this in the stored procedure
                    },
                    User = new UserDto
                    {
                        Id = booking.UserId,
                        Email = booking.Email,
                        FirstName = booking.FirstName,
                        LastName = booking.LastName,
                        Role = user?.Role ?? "user",
                        CreatedAt = user?.CreatedAt ?? DateTime.UtcNow
                    }
                };
            }

            private List<TimeSlot> GenerateTimeSlots()
            {
                var slots = new List<TimeSlot>();
                var startTime = new TimeSpan(6, 0, 0); // 6 AM
                var endTime = new TimeSpan(22, 0, 0);  // 10 PM
                var slotDuration = TimeSpan.FromHours(1);

                for (var time = startTime; time < endTime; time += slotDuration)
                {
                    slots.Add(new TimeSlot
                    {
                        StartTime = time,
                        EndTime = time + slotDuration
                    });
                }

                return slots;
            }

            private class TimeSlot
            {
                public TimeSpan StartTime { get; set; }
                public TimeSpan EndTime { get; set; }
            }
        }
    }