using Application.DtoModels.CommonDto;
using Application.Specifications.Pagination;
using Domain.Models;

namespace Application.Contracts.RepositoryContracts;

public interface IParticipantRepository : IRepositoryBase<Participant>
{
    Task<PagedResult<Participant>> GetParticipantsAsync(PageParams pageParams, Guid eventId);
    Task<Participant?> GetParticipantAsync(Guid eventId, Guid participantId);
}