using Application.DtoModels.ParticipantDto;
using AutoMapper;
using Domain.Models;

namespace Application.MappingProfiles;

public class ParticipantMappingProfile : Profile
{
    public ParticipantMappingProfile()
    {
        CreateMap<Participant, GetParticipantDto>()
            .ForMember(dest => dest.DateOfBirth,
                opts => opts
                    .MapFrom(src => src.DateOfBirth.ToString()));
        
        CreateMap<CreateParticipantDto, Participant>()
            .ForMember(dest => dest.DateOfBirth,
                opts => opts
                    .MapFrom(src => DateOnly.Parse(src.DateOfBirth))); // дата валидна!
    }
}