﻿using System.Linq.Expressions;
using Infrastructure.Repositories.RepositoryContracts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.RepositoriesImplementations;

public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    protected readonly EventHubDbContext Repository;
    protected RepositoryBase(EventHubDbContext repositoryContext)
    {
        Repository = repositoryContext;
    }
    public async Task CreateAsync(T entity) => await Repository.Set<T>().AddAsync(entity);
    public void Delete(T entity) => Repository.Set<T>().Remove(entity);
    public void Update(T entity) => Repository.Set<T>().Update(entity);
    public IQueryable<T> GetByPage(IQueryable<T> query, PageParams pageParams)
    {
        var page = pageParams.Page;
        var pageSize = pageParams.PageSize;
        var skip = (page - 1) * pageSize;
        query = query.Skip(skip).Take(pageSize);
        return query;
    }
    
   
}