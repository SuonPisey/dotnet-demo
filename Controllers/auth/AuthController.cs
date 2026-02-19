using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApi.Models;
using MyApi.Repositories;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;
    private readonly IUserRepository _userService;

    public AuthController(AuthService authService, IUserRepository userService)
    {
        _authService = authService;
        _userService = userService;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<ApiResponse<LoginResponse>>> Login(LoginRequest request)
    {
        var loginResult = await _authService.LoginAsync(request.Username, request.Password);
        if (loginResult == null)
            return Unauthorized(new ApiResponse<LoginResponse>(null, "Invalid username or password", false));

        return Ok(new ApiResponse<LoginResponse>(loginResult, "Login successful"));
    }

    [Authorize]
    [HttpGet("profile")]
    public async Task<ActionResult<ApiResponse<UserModel>>> GetProfile()
    {
        var username = User.Identity?.Name;
        var user = await _userService.GetByUsernameAsync(username!);
        return Ok(new ApiResponse<UserModel>(user, "Profile retrieved"));
    }
}