using Application.Contracts.RepositoryContracts;
using Application.Contracts.UseCaseContracts.ParticipantUseCaseContracts;
using Application.DtoModels.ParticipantDto;
using Application.Exceptions;
using AutoMapper;
using Domain.Models;

namespace Application.UseCases.ParticipantUseCases;

public class CreateParticipantUseCase : ICreateParticipantUseCase 
{
    private readonly IRepositoriesManager _repositoriesManager;
    private readonly IMapper _mapper;
    public CreateParticipantUseCase(IRepositoriesManager repositoriesManager, IMapper mapper)
    {
        _repositoriesManager = repositoriesManager;
        _mapper = mapper;
    }
    
    public async Task<RegistrationResult> Handle(Guid eventId, CreateParticipantDto item, Guid userId)
    {
        var eventDb = await _repositoriesManager.Events.GetByIdAsync(eventId);
        if (eventDb == null)
            throw new EntityNotFoundException($"Event with id {eventId} doesn't exist");
        if (eventDb.Participants.Count >= eventDb.MaxQuantityParticipant)
        {
            return new RegistrationResult
            {
                Message = $"Maximum number of participants reached for event with id {eventId}.",
                Success = false,
                Participant = null
            };
        }
        var participant = _mapper.Map<Participant>(item);
        participant.UserId = userId;
        participant.EventId = eventId;
        participant.RegistrationTime = DateTime.UtcNow;
        await _repositoriesManager.Participants.CreateAsync(participant);
        await _repositoriesManager.SaveAsync();
        var participantDto = _mapper.Map<GetParticipantDto>(participant);
        return new RegistrationResult
        {
            Message = $"You are registered for event with id {eventId}.",
            Success = true,
            Participant = participantDto
        };
    }
}