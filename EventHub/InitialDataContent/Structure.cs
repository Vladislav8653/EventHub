using Entities.Models;

namespace EventHub.InitialData;

public class Structure
{
    public List<Category> Categories = [];
    public List<Event> Events = [];
    public List<EventParticipant> EventParticipants = [];
    public List<Participant> Participants = [];
}