using Application.Contracts.RepositoryContracts;
using Application.DtoModels.CommonDto;
using Application.Specifications.Pagination;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.RepositoryImplementations;

public class ParticipantRepository : RepositoryBase<Participant>, IParticipantRepository
{
    public ParticipantRepository(EventHubDbContext eventHubDbContext) : base(eventHubDbContext){ }

    public async Task<PagedResult<Participant>> GetParticipantsAsync(PageParams pageParams, Guid eventId)
    {
        var query = Repository.Participants.AsQueryable();
        return await GetByPageAsync(query, pageParams);
    }

    public async Task<Participant?> GetParticipantAsync(Guid eventId, Guid participantId) =>
        await Repository.Participants
            .Where(p => p.Id == participantId && p.EventId == eventId)
            .FirstOrDefaultAsync();
}