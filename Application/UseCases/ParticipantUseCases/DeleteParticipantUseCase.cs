using Application.Contracts.RepositoryContracts;
using Application.Contracts.UseCaseContracts.ParticipantUseCaseContracts;
using Application.DtoModels.ParticipantDto;
using Application.Exceptions;
using AutoMapper;

namespace Application.UseCases.ParticipantUseCases;

public class DeleteParticipantUseCase : IDeleteParticipantUseCase
{
    private readonly IRepositoriesManager _repositoriesManager;
    private readonly IMapper _mapper;
    public DeleteParticipantUseCase(IRepositoriesManager repositoriesManager, IMapper mapper)
    {
        _repositoriesManager = repositoriesManager;
        _mapper = mapper;
    }


    public async Task<GetParticipantDto> Handle(Guid eventId, Guid participantId, Guid userId)
    {
        var eventDb = await _repositoriesManager.Events.GetByIdAsync(eventId);
        if (eventDb == null)
            throw new EntityNotFoundException($"Event with id {eventId} doesn't exist");
        var participant = await _repositoriesManager.Participants.GetParticipantAsync(eventId, participantId);
        if(participant == null)
            throw new EntityNotFoundException($"Participant with id {participantId} doesn't exist");
        if (participant.UserId != userId)
            throw new UnauthorizedAccessException("User does not have permission to remove other participants.");
        _repositoriesManager.Participants.Delete(participant);
        await _repositoriesManager.SaveAsync();
        var participantDto = _mapper.Map<GetParticipantDto>(participant);
        return participantDto;
    }
}