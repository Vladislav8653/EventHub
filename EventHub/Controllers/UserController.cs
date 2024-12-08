using System.Security.Claims;
using BusinessLayer.DtoModels.UserDto;
using BusinessLayer.Services.Contracts;
using EventHub.Validation.User.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventHub.Controllers;

[ApiController]
[Route("auth")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    [ServiceFilter(typeof(ValidateRegisterUserRequestAttribute))]
    public async Task<IActionResult> Register([FromBody]RegisterUserRequest request)
    {
        var response = await _userService.Register(request);
        return Ok(response);
    }
    
    [HttpPost("login")]
    [ServiceFilter(typeof(ValidateLoginUserRequestAttribute))]
    public async Task<IActionResult> Login([FromBody] LoginUserRequest request)
    {
        var response = await _userService.Login(request);
        if (!string.IsNullOrEmpty(response.Token))
            HttpContext.Response.Cookies.Append("token", response.Token);
        return Ok(response.Message);
    }
    
    [HttpPost("logout")]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("token"); 
        return Ok(new { message = "Logged out successfully." });
    }
    
    [Authorize]
    [HttpGet("events")]
    public async Task<IActionResult> GetAllUserEvents()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
        {
            return Unauthorized();
        }
        var userId = userIdClaim.Value;
        var events = await _userService.GetAllUserEventsAsync(Request, userId);
        return Ok(events);
    }
}
