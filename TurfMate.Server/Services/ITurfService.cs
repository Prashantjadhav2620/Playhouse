using TurfMate.Server.Models.DTOs;

namespace TurfMate.Server.Services
{
   public interface ITurfService
    {
        Task<List<TurfDto>> GetAllTurfsAsync();
        Task<TurfDto?> GetTurfByIdAsync(int id);
        Task<TurfDto> CreateTurfAsync(CreateTurfRequest createTurfDto);
        Task<TurfDto?> UpdateTurfAsync(int id, UpdateTurfRequest updateTurfDto);
        Task<bool> DeleteTurfAsync(int id);
        Task<List<TurfDto>> GetAvailableTurfsAsync();
    }
}

