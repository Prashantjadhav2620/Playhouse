using TerfMate.Server.Models;

namespace TurfMate.Server.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User> CreateAsync(User user);
        Task<User?> GetByIdAsync(int id);
        Task<List<User>> GetAllAsync();
    }
}