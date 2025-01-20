using Application.Contracts.RepositoryContracts;
using Application.Contracts.UseCaseContracts.ParticipantUseCaseContracts;
using Application.DtoModels.ParticipantDto;
using Application.Exceptions;
using AutoMapper;

namespace Application.UseCases.ParticipantUseCases;

public class GetParticipantUseCase : IGetParticipantUseCase
{
    private readonly IRepositoriesManager _repositoriesManager;
    private readonly IMapper _mapper;
    public GetParticipantUseCase(IRepositoriesManager repositoriesManager, IMapper mapper)
    {
        _repositoriesManager = repositoriesManager;
        _mapper = mapper;
    }
    
    public async Task<GetParticipantDto> Handle(Guid eventId, Guid participantId, CancellationToken cancellationToken)
    {
        var eventDb = await _repositoriesManager.Events.GetByIdAsync(eventId, cancellationToken);
        if (eventDb == null)
            throw new EntityNotFoundException($"Event with id {eventId} doesn't exist");
        var participant = await _repositoriesManager.Participants
            .GetParticipantAsync(eventId, participantId, cancellationToken);
        if(participant == null)
            throw new EntityNotFoundException($"Participant with id {participantId} doesn't exist");
        var participantDto = _mapper.Map<GetParticipantDto>(participant);
        return participantDto;
    }
}