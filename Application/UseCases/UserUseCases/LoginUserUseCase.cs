using Application.Contracts.AuthContracts;
using Application.Contracts.RepositoryContracts;
using Application.Contracts.UseCaseContracts.UserUseCaseContracts;
using Application.DtoModels.UserDto;

namespace Application.UseCases.UserUseCases;

public class LoginUserUseCase : ILoginUserUseCase
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtProvider _jwtProvider;
    private readonly IRepositoriesManager _repositoriesManager;
    private const int RefreshTokenLifeTimeSec = 600; //10 минут
    
    public LoginUserUseCase(IPasswordHasher passwordHasher, IRepositoriesManager repositoriesManager, IJwtProvider jwtProvider)
    {
        _passwordHasher = passwordHasher;
        _repositoriesManager = repositoriesManager;
        _jwtProvider = jwtProvider;
    }
    
    public async Task<LoginUserResponse> Handle(LoginUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _repositoriesManager.Users.GetUserByLoginAsync(request.Login, cancellationToken);
        if (user == null)
        {
            return new LoginUserResponse($"User with login {request.Login} doesn't exists.");
        }
        var result = _passwordHasher.Verify(request.Password, user.Password);
        if (result == false)
        {
            return new LoginUserResponse($"Invalid password.");
        }
        var accessToken = _jwtProvider.GenerateAccessToken(user);
        var refreshToken = _jwtProvider.GenerateRefreshToken();
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpireTime = DateTime.UtcNow.AddSeconds(RefreshTokenLifeTimeSec);
        await _repositoriesManager.SaveAsync();
        return new LoginUserResponse($"Success!", accessToken, refreshToken);
    }
}