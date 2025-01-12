using Domain.Models;
using Infrastructure.Repositories.RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.RepositoriesImplementations;

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
        eventDb.Participants.Add(eventParticipant);
        await CreateAsync(eventParticipant);
    }

    public async Task DetachParticipantFromEvent(Guid eventId, Guid participantId)
    {
        var eventParticipant = await Repository.EventsParticipants
            .Include(ep => ep.Event)
            .FirstAsync(ep => ep.ParticipantId == participantId && ep.EventId == eventId);
        var eventDb = eventParticipant.Event;
        eventDb.Participants.Remove(eventParticipant);
        Delete(eventParticipant);
    }
}