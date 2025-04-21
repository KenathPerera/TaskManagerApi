using TaskManagerApi.Models;

public interface IAuthRepository
{
    Task<bool> UserExistsAsync(string username);
    Task<List<AppRole>> GetRolesByNamesAsync(List<string> roleNames);
    Task AddUserAsync(AppUser user);
    Task<AppUser?> GetUserByUsernameAsync(string username);

}
