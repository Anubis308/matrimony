using MatrimonyAPI.DTOs;

namespace MatrimonyAPI.Services;

public interface IAuthService
{
    Task<LoginResponse?> RegisterAsync(RegisterRequest request);
    Task<LoginResponse?> LoginAsync(LoginRequest request);
    Task<bool> ChangePasswordAsync(string userId, ChangePasswordRequest request);
    string GenerateJwtToken(string userId, string email, string role);
}

