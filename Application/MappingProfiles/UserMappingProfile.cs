using AutoMapper;
using BusinessLayer.DtoModels.UserDto;
using DataLayer.Models;

namespace BusinessLayer.Mapper;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<RegisterUserRequest, User>();
        CreateMap<LoginUserRequest, User>();
    }
}