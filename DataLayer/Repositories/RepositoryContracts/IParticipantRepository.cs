using DataLayer.Models;
using DataLayer.Models.Dto;

namespace DataLayer.Repositories.RepositoryContracts;

public interface IParticipantRepository : IRepositoryBase<Participant>
{
    Task<IEnumerable<ParticipantWithAddInfoDto>> GetParticipantsAsync(Guid eventId);
    Task RegisterParticipantAsync(Event eventDb, Participant participant, DateTime registrationTime);
    Task<ParticipantWithAddInfoDto?> GetParticipantAsync(Guid eventId, Guid participantId);
    Task RemoveParticipantAsync(Guid eventId, Participant participant);
}