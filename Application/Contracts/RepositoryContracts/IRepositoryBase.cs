namespace Application.Contracts.RepositoryContracts;

public interface IRepositoryBase<in T>
{
    Task CreateAsync(T entity, CancellationToken cancellationToken);
    void Delete(T entity);
    void Update(T entity);
}