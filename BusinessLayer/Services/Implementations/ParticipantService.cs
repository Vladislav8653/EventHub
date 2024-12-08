using AutoMapper;
using BusinessLayer.DtoModels.CommonDto;
using BusinessLayer.DtoModels.ParticipantDto;
using BusinessLayer.Exceptions;
using BusinessLayer.Services.Contracts;
using DataLayer.Models;
using DataLayer.Repositories.UnitOfWork;
using DataLayer.Specifications.Pagination;

namespace BusinessLayer.Services.Implementations;

public class ParticipantService : IParticipantService
{
    private readonly IRepositoriesManager _repositoriesManager;
    private readonly IMapper _mapper;
    private const int DefaultPage = 1;
    private const int DefaultPageSize = 5;
    public ParticipantService(IRepositoriesManager repositoriesManager, IMapper mapper)
    {
        _repositoriesManager = repositoriesManager;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetParticipantDto>> GetParticipantsAsync(PageParamsDto? pageParamsDto, Guid eventId)
    {
        var eventDb = await _repositoriesManager.Events.GetByIdAsync(eventId);
        if (eventDb == null)
            throw new EntityNotFoundException($"Event with id {eventId} doesn't exist");
        PageParams? pageParams = null;
        if (pageParamsDto != null)
        { 
            pageParams = new PageParams(
                pageParamsDto.Page,
                pageParamsDto.PageSize,
                DefaultPage,
                DefaultPageSize);
        }
        var participants = await _repositoriesManager.Participants.GetParticipantsAsync(pageParams, eventId);
        var participantsDto = _mapper.Map<IEnumerable<GetParticipantDto>>(participants);
        return participantsDto;
    }


    public async Task<RegistrationResult> RegisterParticipantAsync(Guid eventId, CreateParticipantDto item, string userIdStr)
    {
        var userId = Guid.Parse(userIdStr);
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
        var currentDate = DateTime.UtcNow;
        await _repositoriesManager.Participants.RegisterParticipantAsync(eventDb, participant, currentDate);
        await _repositoriesManager.SaveAsync();
        var participantDto = _mapper.Map<GetParticipantDto>(participant);
        return new RegistrationResult
        {
            Message = $"You are registered for event with id {eventId}.",
            Success = true,
            Participant = participantDto
        };
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

    public async Task<GetParticipantDto> RemoveParticipantAsync(Guid eventId, Guid participantId, string userIdStr)
    {
        var eventDb = await _repositoriesManager.Events.GetByIdAsync(eventId);
        if (eventDb == null)
            throw new EntityNotFoundException($"Event with id {eventId} doesn't exist");
        var participant = await _repositoriesManager.Participants.GetParticipantAsync(eventId, participantId);
        if(participant == null)
            throw new EntityNotFoundException($"Participant with id {participantId} doesn't exist");
        var userId = Guid.Parse(userIdStr);
        if (participant.Participant.UserId == userId)
            throw new UnauthorizedAccessException("User does not have permission to remove other participants.");
        await _repositoriesManager.Participants.RemoveParticipantAsync(eventId, participant.Participant);
        await _repositoriesManager.SaveAsync();
        var participantDto = _mapper.Map<GetParticipantDto>(participant);
        return participantDto;
    }
}