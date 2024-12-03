﻿using DataLayer.Models;
using DataLayer.Models.Filters;

namespace DataLayer.Repositories.RepositoryContracts;

public interface IEventRepository : IRepositoryBase<Event>
{
    Task<bool> IsUniqueNameAsync(string name);
    Task<Event?> GetByNameAsync(string name);
    Task<Event?> GetByIdAsync(Guid id);
    Task<IEnumerable<Event>> GetAllAsync();
    Task<IEnumerable<Event>> GetByFiltersAsync(EventFilters filters);
   
}