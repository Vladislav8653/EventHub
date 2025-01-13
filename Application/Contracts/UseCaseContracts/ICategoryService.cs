using Application.DtoModels.CategoryDto;
using Domain.Models;

namespace Application.Contracts.UseCaseContracts;

public interface ICategoryService
{
    Task<Category?> TryGetByNameAsync(string name);
    Task<Category?> TryGetByIdAsync(Guid id);
    Task<IEnumerable<Category>> GetAllAsync();
    Task<Category> CreateAsync(CategoryDto item);
    
    void Delete(Guid id);
}