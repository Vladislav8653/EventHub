using System.Linq.Expressions;
using DataLayer.Specifications.Pagination;

namespace DataLayer.Repositories.RepositoryContracts;

public interface IRepositoryBase<T>
{
    Task CreateAsync(T entity);
    void Delete(T entity);
    void Update(T entity);
    IQueryable<T> FindAll(bool trackChanges);
    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges);
    IQueryable<T> GetByPage(IQueryable<T> query, PageParams pageParams);
}