using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Data;
using TaskManagerApi.Models;

public class AuthRepository : IAuthRepository
{
    private readonly AppDbContext _context;

    public AuthRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> UserExistsAsync(string username)
    {
        return await _context.Users.AnyAsync(u => u.Username == username);
    }

    public async Task<List<AppRole>> GetRolesByNamesAsync(List<string> roleNames)
    {
        return await _context.Roles
            .Where(r => roleNames.Contains(r.Name))
            .ToListAsync();
    }

    public async Task AddUserAsync(AppUser user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task<AppUser?> GetUserByUsernameAsync(string username)
    {
        return await _context.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Username == username);
    }

}
