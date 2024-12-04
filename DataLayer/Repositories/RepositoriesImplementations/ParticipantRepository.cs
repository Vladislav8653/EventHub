using DataLayer.Repositories.RepositoryContracts;
using DataLayer.Data;
using DataLayer.Models;
using DataLayer.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories.RepositoriesImplementations;

public class ParticipantRepository : RepositoryBase<Participant>, IParticipantRepository
{

    private readonly IEventParticipantRepository _eventParticipantRepository;
    public ParticipantRepository(EventHubDbContext eventHubDbContext, 
        IEventParticipantRepository eventParticipantRepository) : base(eventHubDbContext)
    {
        _eventParticipantRepository = eventParticipantRepository;
    }

    public async Task<IEnumerable<ParticipantWithAddInfoDto>> GetParticipantsAsync(Guid eventId) =>
        await Repository.EventsParticipants
            .Where(ep => ep.EventId == eventId)
            .Include(ep => ep.Participant)
            .Select(ep => new ParticipantWithAddInfoDto()
            {
                Participant = ep.Participant,
                RegistrationTime = ep.RegistrationTime
            })   
            .ToListAsync();

    public async Task<ParticipantWithAddInfoDto?> GetParticipantAsync(Guid eventId, Guid participantId) =>
        await Repository.EventsParticipants
            .Where(ep => ep.EventId == eventId && ep.ParticipantId == participantId)
            .Include(ep => ep.Participant)
            .Select(ep => new ParticipantWithAddInfoDto()
            {
                Participant = ep.Participant,
                RegistrationTime = ep.RegistrationTime
            })
            .FirstOrDefaultAsync();
        
            
        

    public async Task RegisterParticipantAsync(Event eventDb, Participant participant, DateTime regTime)
    {
        await CreateAsync(participant); // сначала мы создаем
        await _eventParticipantRepository.AttachParticipantToEvent(participant, eventDb, regTime); // привязываем к событию
    }
    

    public async Task RemoveParticipantAsync(Guid eventId, Participant participant)
    {
        await _eventParticipantRepository.DetachParticipantFromEvent(eventId, participant.Id); // удалаем связь
        Delete(participant);
    }
}