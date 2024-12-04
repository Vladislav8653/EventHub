using DataLayer.Repositories.RepositoryContracts;
using DataLayer.Data;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories.RepositoriesImplementations;

public class EventParticipantRepository : 
    RepositoryBase<EventParticipant>, IEventParticipantRepository
{
    public EventParticipantRepository(EventHubDbContext eventHubDbContext) : base(eventHubDbContext) { }
    
    public async Task AttachParticipantToEvent(Participant participant, Event eventDb, DateTime regTime)
    {
        //потом привязываем к событию
        var eventParticipant = new EventParticipant()
        {
            Event = eventDb,
            Participant = participant,
            EventId = eventDb.Id,
            ParticipantId = participant.Id,
            RegistrationTime = regTime 
        };
        await CreateAsync(eventParticipant);
    }

    public async Task DetachParticipantFromEvent(Guid eventId, Guid participantId)
    {
        var eventParticipant = await Repository.EventsParticipants.FirstAsync(ep => 
            ep.ParticipantId == participantId && ep.EventId == eventId);
        Delete(eventParticipant);
    }
}