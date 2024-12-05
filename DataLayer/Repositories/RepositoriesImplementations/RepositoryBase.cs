using System.Linq.Expressions;
using DataLayer.Data;
using DataLayer.Repositories.RepositoryContracts;
using DataLayer.Specifications.Pagination;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories.RepositoriesImplementations;

public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    protected EventHubDbContext Repository;

    protected RepositoryBase(EventHubDbContext repositoryContext)
    {
        Repository = repositoryContext;
    }

    public async Task CreateAsync(T entity)
    {
        await Repository.Set<T>().AddAsync(entity);
    }


    public void Delete(T entity) => Repository.Set<T>().Remove(entity);
    

    public void Update(T entity) => Repository.Set<T>().Update(entity);
    

    public IQueryable<T> FindAll(bool trackChanges)
    {
        if (trackChanges)
        {
            return Repository.Set<T>();
        }
        return Repository.Set<T>().AsNoTracking();
    }

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
    {
        if (trackChanges)
        {
            return Repository.Set<T>().Where(expression);
        }
        return Repository.Set<T>().Where(expression).AsNoTracking();
    }
    
    public IQueryable<T> GetByPage(IQueryable<T> query, PageParams pageParams)
    {
        var page = pageParams.Page;
        var pageSize = pageParams.PageSize;
        var skip = (page - 1) * pageSize;
        query = query.Skip(skip).Take(pageSize);
        return query;
    }
   
}