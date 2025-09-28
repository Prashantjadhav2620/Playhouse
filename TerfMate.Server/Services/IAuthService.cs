using TurfMate.Server.Models.DTOs;

namespace TurfMate.Server.Services
{
    public interface IAuthService
    {
        Task<AuthResponse> RegisterAsync(RegisterRequest registerDto);

        Task<AuthResponse> LoginAsync(LoginRequest loginDto);

        string GenerateJwtToken(UserDto user);
    }
}