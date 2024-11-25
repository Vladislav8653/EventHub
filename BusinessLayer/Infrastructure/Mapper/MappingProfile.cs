using AutoMapper;
using BusinessLayer.DtoModels.CategoryDto;
using BusinessLayer.DtoModels.EventsDto;
using DataLayer.Models;

namespace BusinessLayer.Infrastructure.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateEventDto, Event>()
            .ForMember(dest => dest.Category, 
                opts => opts.Ignore())
            .ForMember(dest => dest.DateTime, 
                opts => opts.MapFrom(src => 
                    DateTime.Parse(src.DateTime).ToUniversalTime())); // datetime проверено валидатором как валидное

        CreateMap<Event, GetEventDto>()
            .ForMember(dest => dest.Category,
                opts => opts.MapFrom(src =>
                    src.Category.Name))
            .ForMember(dest => dest.DateTime,
                opts => opts.MapFrom(src =>
                    src.DateTime.ToString("g"))); // можно по-разному вернуть время и дату
        
        
        CreateMap<CategoryDto, Category>();
    }
}