using System.Linq.Expressions;
using Contracts;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    protected RepositoryContext Repository;

    protected RepositoryBase(RepositoryContext repositoryContext)
    {
        Repository = repositoryContext;
    }

    public void Create(T entity)
    {
        Repository.Set<T>().Add(entity);
    }

    public void Delete(T entity)
    {
        Repository.Set<T>().Remove(entity);
    }

    public void Update(T entity)
    {
        Repository.Set<T>().Update(entity);
    }

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
}