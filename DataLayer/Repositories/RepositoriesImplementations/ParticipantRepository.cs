using DataLayer.Repositories.RepositoryContracts;
using DataLayer.Data;
using DataLayer.Models;
using DataLayer.Specifications.Dto.Participants;
using DataLayer.Specifications.Pagination;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories.RepositoriesImplementations;

public class ParticipantRepository : RepositoryBase<Participant>, IParticipantRepository
{
    public ParticipantRepository(EventHubDbContext eventHubDbContext) : base(eventHubDbContext){ }

    public async Task<IEnumerable<ParticipantWithAddInfoDto>> GetParticipantsAsync(PageParams? pageParams, Guid eventId)
    {
        
        var query = Repository.EventsParticipants
            .Where(ep => ep.EventId == eventId)
            .Include(ep => ep.Participant)
            .Select(ep => new ParticipantWithAddInfoDto()
            {
                Participant = ep.Participant,
                RegistrationTime = ep.RegistrationTime
            });
        if (pageParams != null)
            query = GetByPage(query, pageParams);
        return await query.ToListAsync();
    }

    public async Task<ParticipantWithAddInfoDto?> GetParticipantAsync(Guid eventId, Guid participantId) =>
        await Repository.EventsParticipants
            .Where(ep => ep.EventId == eventId && ep.ParticipantId == participantId)
            .Include(ep => ep.Participant)
            .Select(ep => new ParticipantWithAddInfoDto()
            {
                Participant = ep.Participant,
                RegistrationTime = ep.RegistrationTime
            })
            .FirstOrDefaultAsync();
    
    // так как я возвращаю кастомный тип, а не базовый Participant, я не могу использовать этот метод из RepositoryBase
    private IQueryable<ParticipantWithAddInfoDto> GetByPage(IQueryable<ParticipantWithAddInfoDto> query, PageParams pageParams)
    {
        var page = pageParams.Page;
        var pageSize = pageParams.PageSize;
        var skip = (page - 1) * pageSize;
        query = query.Skip(skip).Take(pageSize);
        return query;
    }
}