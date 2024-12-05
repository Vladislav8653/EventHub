using DataLayer.Models;
using DataLayer.Specifications.Dto.Participants;
using DataLayer.Specifications.Pagination;

namespace DataLayer.Repositories.RepositoryContracts;

public interface IParticipantRepository : IRepositoryBase<Participant>
{
    Task<IEnumerable<ParticipantWithAddInfoDto>> GetParticipantsAsync(PageParams? pageParams, Guid eventId);
    Task RegisterParticipantAsync(Event eventDb, Participant participant, DateTime registrationTime);
    Task<ParticipantWithAddInfoDto?> GetParticipantAsync(Guid eventId, Guid participantId);
    Task RemoveParticipantAsync(Guid eventId, Participant participant);
}