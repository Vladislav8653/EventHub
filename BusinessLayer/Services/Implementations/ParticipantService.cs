using AutoMapper;
using BusinessLayer.DtoModels.ParticipantDto;
using BusinessLayer.Exceptions;
using BusinessLayer.Services.Contracts;
using DataLayer.Models;
using DataLayer.Repositories.UnitOfWork;

namespace BusinessLayer.Services.Implementations;

public class ParticipantService : IParticipantService
{
    private readonly IRepositoriesManager _repositoriesManager;
    private readonly IMapper _mapper;
    public ParticipantService(IRepositoriesManager repositoriesManager, IMapper mapper)
    {
        _repositoriesManager = repositoriesManager;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetParticipantDto>> GetParticipantsAsync(Guid eventId)
    {
        var eventDb = await _repositoriesManager.Events.GetByIdAsync(eventId);
        if (eventDb == null)
            throw new EntityNotFoundException($"Event with id {eventId} doesn't exist");
        var participants = await _repositoriesManager.Participants.GetParticipantsAsync(eventId);
        var participantsDto = _mapper.Map<IEnumerable<GetParticipantDto>>(participants);
        return participantsDto;
    }


    public async Task<GetParticipantDto> RegisterParticipantAsync(Guid eventId, CreateParticipantDto item)
    {
        var eventDb = await _repositoriesManager.Events.GetByIdAsync(eventId);
        if (eventDb == null)
            throw new EntityNotFoundException($"Event with id {eventId} doesn't exist");
        var participant = _mapper.Map<Participant>(item);
        var currentDate = DateTime.UtcNow;
        await _repositoriesManager.Participants.RegisterParticipantAsync(eventDb, participant, currentDate);
        await _repositoriesManager.SaveAsync();
        var participantDto = _mapper.Map<GetParticipantDto>(participant);
        return participantDto;
    }

    public async Task<GetParticipantDto> GetParticipantAsync(Guid eventId, Guid participantId)
    {
        var eventDb = await _repositoriesManager.Events.GetByIdAsync(eventId);
        if (eventDb == null)
            throw new EntityNotFoundException($"Event with id {eventId} doesn't exist");
        var participant = await _repositoriesManager.Participants.GetParticipantAsync(eventId, participantId);
        if(participant == null)
            throw new EntityNotFoundException($"Participant with id {participantId} doesn't exist");
        var participantDto = _mapper.Map<GetParticipantDto>(participant);
        return participantDto;
    }

    public async Task<GetParticipantDto> RemoveParticipantAsync(Guid eventId, Guid participantId)
    {
        var eventDb = await _repositoriesManager.Events.GetByIdAsync(eventId);
        if (eventDb == null)
            throw new EntityNotFoundException($"Event with id {eventId} doesn't exist");
        var participant = await _repositoriesManager.Participants.GetParticipantAsync(eventId, participantId);
        if(participant == null)
            throw new EntityNotFoundException($"Participant with id {participantId} doesn't exist");
        await _repositoriesManager.Participants.RemoveParticipantAsync(eventId, participant.Participant);
        await _repositoriesManager.SaveAsync();
        var participantDto = _mapper.Map<GetParticipantDto>(participant);
        return participantDto;
    }
}