using Application.Contracts;
using Application.Contracts.AuthContracts;
using Application.Contracts.UseCaseContracts;
using Application.DtoModels.EventsDto;
using Application.DtoModels.UserDto;
using AutoMapper;
using Domain.Models;

namespace Application.UseCases;

public class UserService : IUserService
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtProvider _jwtProvider;
    private readonly IRepositoriesManager _repositoriesManager;
    private readonly IMapper _mapper;
    private enum Roles { User, Admin }
    
    public UserService(IPasswordHasher passwordHasher, IRepositoriesManager repositoriesManager, IJwtProvider jwtProvider, IMapper mapper)
    {
        _passwordHasher = passwordHasher;
        _repositoriesManager = repositoriesManager;
        _jwtProvider = jwtProvider;
        _mapper = mapper;
    }

    public async Task<RegisterUserResponse> Register(RegisterUserRequest request)
    {
        var checkUser = await _repositoriesManager.Users.GetUserByLoginAsync(request.Login);
        if (checkUser != null)
            return new RegisterUserResponse($"User with login {request.Login} already exists.");
        if (!Enum.GetNames(typeof(Roles)).Contains(request.Role))
        {
            return new RegisterUserResponse($"Role {request.Role} doesn't exists. There are {Roles.User} and {Roles.Admin} only.");
        }
        var hashedPassword = _passwordHasher.Generate(request.Password);
        var user = _mapper.Map<User>(request);
        user.Password = hashedPassword;
        await _repositoriesManager.Users.CreateAsync(user);
        await _repositoriesManager.SaveAsync();
        return new RegisterUserResponse($"User with login {request.Login} created!");
    }


    public async Task<LoginUserResponse> Login(LoginUserRequest request)
    {
        var user = await _repositoriesManager.Users.GetUserByLoginAsync(request.Login);
        if (user == null)
        {
            return new LoginUserResponse($"User with login {request.Login} doesn't exists.");
        }

        var result = _passwordHasher.Verify(request.Password, user.Password);

        if (result == false)
        {
            return new LoginUserResponse($"Invalid password.");
        }

        var token = _jwtProvider.GenerateToken(user);
        return new LoginUserResponse($"Success!", token);
    }
    
    public async Task<IEnumerable<GetEventDto>> GetAllUserEventsAsync(HttpRequest request, string userIdStr)
    {
        var userId = Guid.Parse(userIdStr);
        var events = await _repositoriesManager.Events.GetAllUserEventsAsync(userId);
        foreach (var eventDb in events)
        {
            if (!string.IsNullOrEmpty(eventDb.Image))
                eventDb.Image = new Uri(new Uri($"{request.Scheme}://{request.Host}"), $"events/images/{eventDb.Image}").ToString();
        }
        var eventsWithImages = _mapper.Map<IEnumerable<GetEventDto>>(events);
        return eventsWithImages;
    }
}