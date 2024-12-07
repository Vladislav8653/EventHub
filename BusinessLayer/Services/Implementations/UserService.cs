using BusinessLayer.DtoModels.UserDto;
using BusinessLayer.Services.Contracts;
using BusinessLayer.Services.Contracts.Auth;
using DataLayer.Models;
using DataLayer.Repositories.UnitOfWork;

namespace BusinessLayer.Services.Implementations;

public class UserService : IUserService
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtProvider _jwtProvider;
    private readonly IRepositoriesManager _repositoriesManager;
    
    public UserService(IPasswordHasher passwordHasher, IRepositoriesManager repositoriesManager, IJwtProvider jwtProvider)
    {
        _passwordHasher = passwordHasher;
        _repositoriesManager = repositoriesManager;
        _jwtProvider = jwtProvider;
    }

    public async Task Register(RegisterUserRequest request)
    {
        var hashedPassword = _passwordHasher.Generate(request.Password);
        var user = new User // потом через маппер 
        {
            Login = request.Login,
            Password = hashedPassword,
            UserName = request.UserName
        };
        await _repositoriesManager.Users.CreateAsync(user);
        await _repositoriesManager.SaveAsync();
    }


    public async Task<string> Login(LoginUserRequest request)
    {
        var user = await _repositoriesManager.Users.GetUserByLoginAsync(request.Login);
        if (user == null)
        {
            throw new Exception("zalupa1");
            // пототм сделаю норм
        }

        var result = _passwordHasher.Verify(request.Password, user.Password);

        if (result == false)
        {
            throw new Exception("zalupa2");
            // пототм сделаю норм
        }

        var token = _jwtProvider.GenerateToken(user);
        return token;
    }
}