using Application.Contracts;
using Application.Contracts.UseCaseContracts.UserUseCaseContracts;
using Application.DtoModels.UserDto;
using Application.Validation.User.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("auth")]
public class UserController : ControllerBase
{
    private readonly ICookieService _cookieService;
    private readonly IRefreshTokenUseCase _refreshTokenUseCase;
    private readonly ILoginUserUseCase _loginUserUseCase;
    private readonly IRegisterUserUseCase _registerUserUseCase;
    public const string AccessTokenCookieName = "access-token";
    public const string RefreshTokenCookieName = "refresh-token";

    public UserController(ICookieService cookieService, IRefreshTokenUseCase refreshTokenUseCase, 
        ILoginUserUseCase loginUserUseCase, IRegisterUserUseCase registerUserUseCase)
    {
        _cookieService = cookieService;
        _refreshTokenUseCase = refreshTokenUseCase;
        _loginUserUseCase = loginUserUseCase;
        _registerUserUseCase = registerUserUseCase;
    }

    [HttpPost("register")]
    [ServiceFilter(typeof(ValidateRegisterUserRequestAttribute))]
    public async Task<IActionResult> Register([FromBody]RegisterUserRequest request, CancellationToken cancellationToken)
    {
        var response = await _registerUserUseCase.Handle(request, cancellationToken);
        return Ok(response);
    }
    
    [HttpPost("login")]
    [ServiceFilter(typeof(ValidateLoginUserRequestAttribute))]
    public async Task<IActionResult> Login([FromBody] LoginUserRequest request, CancellationToken cancellationToken)
    {
        var response = await _loginUserUseCase.Handle(request, cancellationToken);
        _cookieService.AddCookie(Response, AccessTokenCookieName, response.AccessToken);
        _cookieService.AddCookie(Response, RefreshTokenCookieName, response.RefreshToken);
        return Ok(response.Message);
    }
    
    [HttpPost("logout")]
    public IActionResult Logout()
    {
        _cookieService.DeleteCookie(Response, AccessTokenCookieName);
        _cookieService.DeleteCookie(Response, RefreshTokenCookieName);
        return Ok(new { message = "Logged out successfully." });
    }
    
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(CancellationToken cancellationToken)
    {
        var refreshToken = _cookieService.GetCookie(Request, RefreshTokenCookieName);
        var oldAccessToken = _cookieService.GetCookie(Request, AccessTokenCookieName);
        var newAccessToken = await _refreshTokenUseCase.Handle(oldAccessToken, refreshToken, cancellationToken);
        _cookieService.AddCookie(Response, AccessTokenCookieName, newAccessToken);
        return Ok("Access token updated");
    }
}
