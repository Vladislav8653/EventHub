using Application.Contracts.RepositoryContracts;
using Application.Specifications.Dto.Participants;
using Application.Specifications.Pagination;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.RepositoryImplementations;

public class ParticipantRepository : RepositoryBase<Participant>, IParticipantRepository
{
    public ParticipantRepository(EventHubDbContext eventHubDbContext) : base(eventHubDbContext){ }

    public async Task<IEnumerable<ParticipantWithAddInfoDto>> GetParticipantsAsync(PageParams pageParams, Guid eventId)
    {
        var query = Repository.EventsParticipants
            .Where(ep => ep.EventId == eventId)
            .Include(ep => ep.Participant)
            .Select(ep => new ParticipantWithAddInfoDto(ep.Participant, ep.RegistrationTime));
        return await GetByPageAsync(query, pageParams);
    }

    public async Task<ParticipantWithAddInfoDto?> GetParticipantAsync(Guid eventId, Guid participantId) =>
        await Repository.EventsParticipants
            .Where(ep => ep.EventId == eventId && ep.ParticipantId == participantId)
            .Include(ep => ep.Participant)
            .Select(ep => new ParticipantWithAddInfoDto(ep.Participant, ep.RegistrationTime))
            .FirstOrDefaultAsync();
    
     private async Task<IEnumerable<ParticipantWithAddInfoDto>> GetByPageAsync(IQueryable<ParticipantWithAddInfoDto> query, PageParams pageParams)
    {
        var page = pageParams.Page;
        var pageSize = pageParams.PageSize;
        var skip = (page - 1) * pageSize;
        query = query.Skip(skip).Take(pageSize);
        return await query.ToListAsync();
    }
}