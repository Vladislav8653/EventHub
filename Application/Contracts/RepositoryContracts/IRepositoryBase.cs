using Application.Specifications.Pagination;

namespace Application.Contracts.RepositoryContracts;

public interface IRepositoryBase<T>
{
    Task CreateAsync(T entity);
    void Delete(T entity);
    void Update(T entity);
    IQueryable<T> GetByPage(IQueryable<T> query, PageParams pageParams);
}