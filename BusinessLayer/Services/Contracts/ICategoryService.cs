using BusinessLayer.DtoModels;
using BusinessLayer.DtoModels.CategoryDto;
using DataLayer.Models;

namespace BusinessLayer.Services.Contracts;

public interface ICategoryService
{
    Task<Category?> TryGetByNameAsync(string name);
    Category? TryGetById(Guid id);
    bool Exists(Guid id);
    Task<IEnumerable<Category>> GetAllAsync();
    Task<Category> CreateAsync(CategoryDto item);
    
    void Delete(Guid id);
}