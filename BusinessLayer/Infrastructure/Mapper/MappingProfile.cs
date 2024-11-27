using System.Runtime.InteropServices.ComTypes;
using AutoMapper;
using BusinessLayer.DtoModels.CategoryDto;
using BusinessLayer.DtoModels.EventsDto;
using DataLayer.Models;
using DataLayer.Models.Filters;

namespace BusinessLayer.Infrastructure.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateEventDto, Event>()
            .ForMember(dest => dest.Category, 
                opts => opts
                    .Ignore())
            .ForMember(dest => dest.DateTime, 
                opts => opts
                    .MapFrom(src => DateTime.Parse(src.DateTime).ToUniversalTime())); // datetime проверено валидатором как валидное

        CreateMap<Event, GetEventDto>()
            .ForMember(dest => dest.Category,
                opts => opts
                    .MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.DateTime,
                opts => opts
                    .MapFrom(src => src.DateTime.ToString("g"))); // можно по-разному вернуть время и дату
        
        
        CreateMap<EventFiltersDto, EventFilters>()
            .ForMember(dest => dest.Date, 
                opts => opts
                    .MapFrom(src => 
                    string.IsNullOrEmpty(src.Date) ? (DateTime?)null : DateTime.Parse(src.Date)))
            .ForMember(dest => dest.StartDate, opts => 
                opts.MapFrom(src => 
                    string.IsNullOrEmpty(src.StartDate) ? (DateTime?)null : DateTime.Parse(src.StartDate)))
            .ForMember(dest => dest.FinishDate, opts => 
                opts.MapFrom(src => 
                    string.IsNullOrEmpty(src.FinishDate) ? (DateTime?)null : DateTime.Parse(src.FinishDate)))
            .ForMember(dest => dest.Place, opts => 
                opts.MapFrom(src => src.Place))
            .ForMember(dest => dest.Category, opts => 
                opts.Ignore());
        
        
        CreateMap<CategoryDto, Category>();
    }
}