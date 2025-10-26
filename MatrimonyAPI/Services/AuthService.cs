using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using MatrimonyAPI.Data;
using MatrimonyAPI.DTOs;
using MatrimonyAPI.Models;

namespace MatrimonyAPI.Services;

public class AuthService : IAuthService
{
    private readonly MongoDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthService(MongoDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<LoginResponse?> RegisterAsync(RegisterRequest request)
    {
        // Check if user already exists
        var existingUser = await _context.Users
            .Find(u => u.Email == request.Email)
            .FirstOrDefaultAsync();

        if (existingUser != null)
        {
            return null;
        }

        // Hash password
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        // Create user
        var user = new User
        {
            Email = request.Email,
            PasswordHash = passwordHash,
            PhoneNumber = request.PhoneNumber,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };

        await _context.Users.InsertOneAsync(user);

        // Generate token
        var token = GenerateJwtToken(user.Id, user.Email, user.Role.ToString());

        return new LoginResponse
        {
            Token = token,
            UserId = user.Id,
            Email = user.Email,
            IsProfileComplete = false
        };
    }

    public async Task<LoginResponse?> LoginAsync(LoginRequest request)
    {
        var user = await _context.Users
            .Find(u => u.Email == request.Email)
            .FirstOrDefaultAsync();

        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            return null;
        }

        if (!user.IsActive)
        {
            return null;
        }

        // Check if profile exists
        var profile = await _context.Profiles
            .Find(p => p.UserId == user.Id)
            .FirstOrDefaultAsync();

        // Update last login
        var update = Builders<User>.Update.Set(u => u.LastLoginAt, DateTime.UtcNow);
        await _context.Users.UpdateOneAsync(u => u.Id == user.Id, update);

        // Generate token
        var token = GenerateJwtToken(user.Id, user.Email, user.Role.ToString());

        return new LoginResponse
        {
            Token = token,
            UserId = user.Id,
            Email = user.Email,
            IsProfileComplete = profile?.IsProfileComplete ?? false
        };
    }

    public async Task<bool> ChangePasswordAsync(string userId, ChangePasswordRequest request)
    {
        var user = await _context.Users
            .Find(u => u.Id == userId)
            .FirstOrDefaultAsync();

        if (user == null)
        {
            return false;
        }

        if (!BCrypt.Net.BCrypt.Verify(request.OldPassword, user.PasswordHash))
        {
            return false;
        }

        var newPasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
        var update = Builders<User>.Update.Set(u => u.PasswordHash, newPasswordHash);
        await _context.Users.UpdateOneAsync(u => u.Id == userId, update);

        return true;
    }

    public string GenerateJwtToken(string userId, string email, string role)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var secretKey = jwtSettings["SecretKey"] ?? throw new InvalidOperationException("JWT SecretKey not configured");
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId),
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Role, role)
        };

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
