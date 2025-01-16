namespace Application.Contracts.RepositoryContracts;

public interface IRepositoryBase<in T>
{
    Task CreateAsync(T entity);
    void Delete(T entity);
    void Update(T entity);
}