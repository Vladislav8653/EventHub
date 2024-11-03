using Entities.Models;

namespace Contracts.RepositoryContracts;

public interface ICategoryRepository
{
    void CreateCategory(Category category);
    void DeleteCategory(Category category);
}