﻿using Application.Contracts.RepositoryContracts;
using Application.DtoModels.CommonDto;
using Application.Specifications.Dto;
using Application.Specifications.Pagination;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.RepositoryImplementations;

public class EventRepository : RepositoryBase<Event>, IEventRepository
{
    public EventRepository(EventHubDbContext eventHubDbContext) : base(eventHubDbContext) { }
    
    public async Task<bool> IsUniqueNameAsync(string name) => 
        !await Repository.Events.AnyAsync(e => e.Name == name);

    public async Task<Event?> GetByNameAsync(string name) =>
        await Repository.Events.FirstOrDefaultAsync(e => e.Name == name);

    public async Task<Event?> GetByIdAsync(Guid id) =>
        await Repository.Events
            .Include(e => e.Category)
            .Include(e => e.Participants)
            .FirstOrDefaultAsync(e => e.Id == id);
    
    public async Task<PagedResult<Event>> GetAllByParamsAsync(EventQueryParams eventParams)
    {
        var query = Repository.Events.Include(e => e.Category).AsQueryable();
        var filters = eventParams.Filters ?? null;
        if (filters != null) 
        {   // query - итоговый результат после всех "ограничений"
            if (filters.Date.HasValue) // если событие в эту дату
                query = query.Where(e => e.DateTime == filters.Date);
            if (filters is { StartDate: not null, FinishDate: not null }) // если событие [с...по]
                query = query.Where(e => e.DateTime > filters.StartDate && e.DateTime < filters.FinishDate);
            if (filters.Category != null)
                query = query.Where(e => e.Category == filters.Category);
            if (!string.IsNullOrEmpty(filters.Place))
                query = query.Where(e => e.Place == filters.Place);
        }
        var pagedResult = await GetByPageAsync(query, eventParams.PageParams);
        return pagedResult;
    }

    public async Task<IEnumerable<Event>> GetAllUserEventsAsync(Guid userId)
    {
        var participantIds = Repository.Participants
            .Where(p => p.UserId == userId)
            .Select(p => p.Id);
        var events = Repository.EventsParticipants
            .Where(ep => participantIds.Contains(ep.ParticipantId))
            .Include(ep => ep.Event)
            .Select(ep => ep.Event);
        return await events.ToListAsync();
    }
}