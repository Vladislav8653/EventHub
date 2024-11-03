using Contracts.RepositoryContracts;
using Entities;
using Entities.Models;

namespace Repository.Repositories;

public class CategoryRepository : RepositoryBase<Category>,
    ICategoryRepository
{
    
    public CategoryRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public void CreateCategory(Category category) => Create(category);

    public void DeleteCategory(Category category) => Delete(category);
}