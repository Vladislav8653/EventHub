using BusinessLayer.DtoModels.UserDto;
using BusinessLayer.Services.Contracts;
using EventHub.Validation.User.Attributes;
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
        HttpContext.Response.Cookies.Append("token", response.Message);
        return Ok(response);
    }
}
