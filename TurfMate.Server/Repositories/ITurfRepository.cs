using TerfMate.Server.Models;

namespace TurfMate.Server.Repositories
{
    public interface ITurfRepository
    {
        Task<List<Turf>> GetAllAsync();
        Task<Turf?> GetByIdAsync(int id);
        Task<Turf> CreateAsync(Turf turf);
        Task<Turf?> UpdateAsync(int id, Turf turf);
        Task<bool> DeleteAsync(int id);
        Task<List<Turf>> GetAvailableTurfsAsync();
    }
}