using Application.DtoModels.CategoryDto;
using AutoMapper;
using Domain.Models;

namespace Application.MappingProfiles;

public class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
    {
        CreateMap<CategoryDto, Category>();
    }
}