using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Dtos.Account;
using TaskManagerApi.Services;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var error = await _authService.RegisterAsync(dto);
        if (error != null)
            return BadRequest(error);

        return Ok(new { message = "User registered successfully" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var token = await _authService.LoginAsync(dto);
        if (token == null)
            return Unauthorized("Invalid username or password");

        return Ok(new { token });
    }

}
