using Application.DtoModels.ParticipantDto;
using Application.Specifications.Dto.Participants;
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

        CreateMap<ParticipantWithAddInfoDto, GetParticipantDto>()
            .ForMember(dest => dest.RegistrationTime,
                opts => opts
                    .MapFrom(src => src.RegistrationTime.ToString("g")))
            .ForMember(dest => dest.Id,
                opts => opts
                    .MapFrom(src => src.Participant.Id))
            .ForMember(dest => dest.Name,
                opts => opts
                    .MapFrom(src => src.Participant.Name))
            .ForMember(dest => dest.Surname,
                opts => opts
                    .MapFrom(src => src.Participant.Surname))
            .ForMember(dest => dest.DateOfBirth,
                opts => opts
                    .MapFrom(src => src.Participant.DateOfBirth.ToString("d")))
            .ForMember(dest => dest.Email,
                opts => opts
                    .MapFrom(src => src.Participant.Email));
        
        CreateMap<CreateParticipantDto, Participant>()
            .ForMember(dest => dest.DateOfBirth,
                opts => opts
                    .MapFrom(src => DateOnly.Parse(src.DateOfBirth))); // дата валидна!
    }
}