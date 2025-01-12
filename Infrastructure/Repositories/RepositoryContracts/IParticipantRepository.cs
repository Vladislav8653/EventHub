using Domain.Models;

namespace Infrastructure.Repositories.RepositoryContracts;

public interface IParticipantRepository : IRepositoryBase<Participant>
{
    Task<IEnumerable<ParticipantWithAddInfoDto>> GetParticipantsAsync(PageParams? pageParams, Guid eventId);
    Task<ParticipantWithAddInfoDto?> GetParticipantAsync(Guid eventId, Guid participantId);
}