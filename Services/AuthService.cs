using System.Security.Cryptography;
using System.Text;
using TaskManagerApi.Dtos.Account;
using TaskManagerApi.Models;
using TaskManagerApi.Repositories;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using TaskManagerApi.Enums;
using TaskManagerApi.Services;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepo;
    private readonly IConfiguration _config;
    private readonly ILogService _logService;

    public AuthService(IAuthRepository authRepo, IConfiguration config, ILogService logService)
    {
        _authRepo = authRepo;
        _config = config;
        _logService = logService;
    }

    public async Task<string?> RegisterAsync(RegisterDto dto)
    {
        if (await _authRepo.UserExistsAsync(dto.Username))
            return "Username already exists";

        using var hmac = new HMACSHA512();

        var user = new AppUser
        {
            Username = dto.Username,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password)),
            PasswordSalt = hmac.Key,
            UserRoles = new List<AppUserRole>()
        };

        var roles = await _authRepo.GetRolesByNamesAsync(dto.Roles);
        if (roles.Count != dto.Roles.Count)
            return "One or more roles are invalid";

        foreach (var role in roles)
        {
            user.UserRoles.Add(new AppUserRole { User = user, Role = role });
        }

        await _authRepo.AddUserAsync(user);
        return null; // success
    }

    public async Task<string?> LoginAsync(LoginDto dto)
    {
        var user = await _authRepo.GetUserByUsernameAsync(dto.Username);
        if (user == null)
            return null;

        using var hmac = new HMACSHA512(user.PasswordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password));

        if (!computedHash.SequenceEqual(user.PasswordHash))
            return null;

        var token = CreateJwtToken(user);
        await _logService.LogAsync(LogEntry.Login, "User", user.Id);
        return token;
    }

    private string CreateJwtToken(AppUser user)
    {
        var claims = new List<Claim>
        {
    new Claim("name", user.Username),
};

        claims.AddRange(user.UserRoles.Select(ur =>
            new Claim("role", ur.Role.Name)));

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddHours(3),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}
