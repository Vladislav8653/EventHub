using Application.DtoModels.CommonDto;
using Domain.Models;

namespace Domain.RepositoryContracts;

public interface IParticipantRepository : IRepositoryBase<Participant>
{
    Task<PagedResult<Participant>> GetParticipantsAsync(PageParams pageParams, Guid eventId, CancellationToken cancellationToken);
    Task<Participant?> GetParticipantAsync(Guid eventId, Guid participantId, CancellationToken cancellationToken);
}