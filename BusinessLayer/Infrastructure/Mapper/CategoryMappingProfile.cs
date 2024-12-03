using AutoMapper;
using BusinessLayer.DtoModels.CategoryDto;
using DataLayer.Models;

namespace BusinessLayer.Infrastructure.Mapper;

public class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
    {
        CreateMap<CategoryDto, Category>();
    }
}