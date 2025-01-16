using Application.Specifications.Dto.Participants;
using Application.Specifications.Pagination;
using Domain.Models;

namespace Application.Contracts.RepositoryContracts;

public interface IParticipantRepository : IRepositoryBase<Participant>
{
    Task<IEnumerable<ParticipantWithAddInfoDto>> GetParticipantsAsync(PageParams pageParams, Guid eventId);
    Task<ParticipantWithAddInfoDto?> GetParticipantAsync(Guid eventId, Guid participantId);
}