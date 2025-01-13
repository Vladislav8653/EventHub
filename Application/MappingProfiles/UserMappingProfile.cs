using Application.DtoModels.UserDto;
using AutoMapper;
using Domain.Models;

namespace Application.MappingProfiles;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<RegisterUserRequest, User>();
        CreateMap<LoginUserRequest, User>();
    }
}