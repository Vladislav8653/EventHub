using Application.Contracts.AuthContracts;
using Application.Contracts.RepositoryContracts;
using Application.Contracts.UseCaseContracts.UserUseCaseContracts;
using Application.DtoModels.UserDto;
using AutoMapper;
using Domain.Models;

namespace Application.UseCases.UserUseCases;

public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtProvider _jwtProvider;
    private readonly IRepositoriesManager _repositoriesManager;
    private readonly IMapper _mapper;
    private enum Roles { User, Admin }
    
    public RegisterUserUseCase(IPasswordHasher passwordHasher, IRepositoriesManager repositoriesManager, IJwtProvider jwtProvider, IMapper mapper)
    {
        _passwordHasher = passwordHasher;
        _repositoriesManager = repositoriesManager;
        _jwtProvider = jwtProvider;
        _mapper = mapper;
    }
    
    public async Task<RegisterUserResponse> Handle(RegisterUserRequest request)
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
}