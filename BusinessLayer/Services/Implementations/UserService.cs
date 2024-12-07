using AutoMapper;
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
    private readonly IMapper _mapper;
    
    public UserService(IPasswordHasher passwordHasher, IRepositoriesManager repositoriesManager, IJwtProvider jwtProvider, IMapper mapper)
    {
        _passwordHasher = passwordHasher;
        _repositoriesManager = repositoriesManager;
        _jwtProvider = jwtProvider;
        _mapper = mapper;
    }

    public async Task<UserResponse> Register(RegisterUserRequest request)
    {
        var checkUser = await _repositoriesManager.Users.GetUserByLoginAsync(request.Login);
        if (checkUser != null)
            return new UserResponse($"User with login {request.Login} already exists.");
        var hashedPassword = _passwordHasher.Generate(request.Password);
        var user = _mapper.Map<User>(request);
        user.Password = hashedPassword;
        await _repositoriesManager.Users.CreateAsync(user);
        await _repositoriesManager.SaveAsync();
        return new UserResponse($"User with login {request.Login} created!");
    }


    public async Task<UserResponse> Login(LoginUserRequest request)
    {
        var user = await _repositoriesManager.Users.GetUserByLoginAsync(request.Login);
        if (user == null)
        {
            return new UserResponse($"User with login {request.Login} doesn't exists.");
        }

        var result = _passwordHasher.Verify(request.Password, user.Password);

        if (result == false)
        {
            return new UserResponse($"Invalid password.");
        }

        var token = _jwtProvider.GenerateToken(user);
        return new UserResponse(token);
    }
}