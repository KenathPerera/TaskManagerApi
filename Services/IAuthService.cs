using TaskManagerApi.Dtos.Account;

public interface IAuthService
{
    Task<string?> RegisterAsync(RegisterDto dto);
    Task<string?> LoginAsync(LoginDto dto);

}

