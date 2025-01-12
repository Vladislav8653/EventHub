﻿using System.Linq.Expressions;

namespace Infrastructure.Repositories.RepositoryContracts;

public interface IRepositoryBase<T>
{
    Task CreateAsync(T entity);
    void Delete(T entity);
    void Update(T entity);
    IQueryable<T> GetByPage(IQueryable<T> query, PageParams pageParams);
}