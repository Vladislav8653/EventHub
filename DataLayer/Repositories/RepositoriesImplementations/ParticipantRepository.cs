using DataLayer.Repositories.RepositoryContracts;
using DataLayer.Data;
using DataLayer.Models;
using DataLayer.Specifications.Dto.Participants;
using DataLayer.Specifications.Pagination;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories.RepositoriesImplementations;

public class ParticipantRepository : RepositoryBase<Participant>, IParticipantRepository
{
    private readonly IEventParticipantRepository _eventParticipantRepository;
    public ParticipantRepository(EventHubDbContext eventHubDbContext, 
        IEventParticipantRepository eventParticipantRepository) : base(eventHubDbContext)
    {
        _eventParticipantRepository = eventParticipantRepository;
    }

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
        
            
        

    public async Task RegisterParticipantAsync(Event eventDb, Participant participant, DateTime regTime)
    {
        // используется транзакция, так как participant существует только у события, 
        // сама по себе сущность бессмысленна. Поэтому связь с событием обязательна.
        using var transaction = await Repository.Database.BeginTransactionAsync();
        try
        {
            await CreateAsync(participant); // сначала мы создаем
            await _eventParticipantRepository.AttachParticipantToEvent(participant, eventDb, regTime); // привязываем к событию
            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
    

    public async Task RemoveParticipantAsync(Guid eventId, Participant participant)
    {
        // используется транзакция, так как participant существует только у события, 
        // сама по себе сущность бессмысленна. Поэтому связь с событием обязательна.
        using var transaction = await Repository.Database.BeginTransactionAsync();
        try
        {
            await _eventParticipantRepository.DetachParticipantFromEvent(eventId, participant.Id); // удалаем связь
            Delete(participant);
            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
    
    
    
    
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