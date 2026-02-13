using Microsoft.AspNetCore.Mvc;
using MyApi.Data;
using MyApi.Models;
[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly AppDbContext _context;

    public UserController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("all")]
    public async Task<ActionResult<ApiResponse<List<UserModel>>>> GetAllUsers()
    {
        var userService = new UserService(_context);
        var users = userService.GetAll();
        var response = new ApiResponse<List<UserModel>>(users, "Users retrieved successfully");
        return Ok(response);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<UserModel>>> GetUserById(int id)
    {
        var userService = new UserService(_context);
        var user = userService.GetById(id);
        if (user == null)
        {
            return NotFound(new ApiResponse<UserModel>(null, "User not found", false));
        }
        var response = new ApiResponse<UserModel>(user, "User retrieved successfully");
        return Ok(response);
    }

    [HttpPost("create")]
    public async Task<ActionResult<ApiResponse<UserModel>>> CreateUser(UserModel user)
    {
        var userService = new UserService(_context);
        var createdUser = userService.Create(user);
        var response = new ApiResponse<UserModel>(createdUser, "User created successfully");
        return Ok(response);
    }

}