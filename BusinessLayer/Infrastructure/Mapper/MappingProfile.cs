using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace EventHub;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Event, EventDto>()
            .ForMember(ed => ed.Category,
                opt => opt.MapFrom(e => e.Category.CategoryName));
    }
}