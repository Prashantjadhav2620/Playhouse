using Dapper;
using System.Data;
using TerfMate.Server.Models;
using TurfMate.Server.Repositories;
using TurfMate.Server.Services;

namespace TerfMate.Server.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly IDatabaseService _databaseService;

        public BookingRepository(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<Booking> CreateAsync(Booking booking)
        {
            using var connection = _databaseService.CreateConnection();
            const string sql = "spBookings_Create";
            var parameters = new DynamicParameters();
            parameters.Add("UserId", booking.UserId);
            parameters.Add("TurfId", booking.TurfId);
            parameters.Add("BookingDate", booking.BookingDate);
            parameters.Add("StartTime", booking.StartTime);
            parameters.Add("EndTime", booking.EndTime);
            parameters.Add("TotalAmount", booking.TotalAmount);
            parameters.Add("Status", booking.Status);
            parameters.Add("Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await connection.ExecuteAsync(sql, parameters, commandType: CommandType.StoredProcedure);

            booking.Id = parameters.Get<int>("Id");
            return booking;
        }

        public async Task<List<BookingWithDetails>> GetByUserIdAsync(int userId)
        {
            using var connection = _databaseService.CreateConnection();
            const string sql = "spBookings_GetByUserId";
            var bookings = await connection.QueryAsync<BookingWithDetails>(
                sql,
                new { UserId = userId },
                commandType: CommandType.StoredProcedure);

            return bookings.ToList();
        }

        public async Task<List<BookingWithDetails>> GetAllAsync()
        {
            using var connection = _databaseService.CreateConnection();
            const string sql = "spBookings_GetAll";
            var bookings = await connection.QueryAsync<BookingWithDetails>(sql, commandType: CommandType.StoredProcedure);
            return bookings.ToList();
        }

        public async Task<Booking?> GetByIdAsync(int id)
        {
            using var connection = _databaseService.CreateConnection();
            const string sql = "SELECT * FROM Bookings WHERE Id = @Id";
            return await connection.QueryFirstOrDefaultAsync<Booking>(sql, new { Id = id });
        }

        public async Task<BookingWithDetails?> GetByIdWithDetailsAsync(int id)
        {
            using var connection = _databaseService.CreateConnection();
            const string sql = "spBookings_GetById";
            return await connection.QueryFirstOrDefaultAsync<BookingWithDetails>(
                sql,
                new { Id = id },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<Booking?> UpdateAsync(int id, Booking updatedBooking)
        {
            using var connection = _databaseService.CreateConnection();
            const string sql = "spBookings_UpdateStatus";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id);
            parameters.Add("Status", updatedBooking.Status);

            var affectedRows = await connection.ExecuteAsync(sql, parameters, commandType: CommandType.StoredProcedure);

            if (affectedRows > 0)
            {
                updatedBooking.Id = id;
                return updatedBooking;
            }

            return null;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = _databaseService.CreateConnection();
            const string sql = "spBookings_Delete";
            var affectedRows = await connection.ExecuteAsync(
                sql,
                new { Id = id },
                commandType: CommandType.StoredProcedure);

            return affectedRows > 0;
        }

        public async Task<bool> IsTimeSlotAvailableAsync(int turfId, DateTime bookingDate, TimeSpan startTime, TimeSpan endTime, int? excludeBookingId = null)
        {
            using var connection = _databaseService.CreateConnection();
            const string sql = "spBookings_CheckTimeSlotAvailability";
            var parameters = new DynamicParameters();
            parameters.Add("TurfId", turfId);
            parameters.Add("BookingDate", bookingDate.Date);
            parameters.Add("StartTime", startTime);
            parameters.Add("EndTime", endTime);
            parameters.Add("ExcludeBookingId", excludeBookingId);
            parameters.Add("IsAvailable", dbType: DbType.Boolean, direction: ParameterDirection.Output);

            await connection.ExecuteAsync(sql, parameters, commandType: CommandType.StoredProcedure);

            return parameters.Get<bool>("IsAvailable");
        }

        public async Task<List<Booking>> GetBookingsByTurfAndDateAsync(int turfId, DateTime date)
        {
            using var connection = _databaseService.CreateConnection();
            const string sql = "spBookings_GetByTurfAndDate";
            var bookings = await connection.QueryAsync<Booking>(
                sql,
                new { TurfId = turfId, BookingDate = date.Date },
                commandType: CommandType.StoredProcedure);

            return bookings.ToList();
        }
    }
}
