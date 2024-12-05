using AutoMapper;
using BusinessLayer.DtoModels.CategoryDto;
using BusinessLayer.Exceptions;
using BusinessLayer.Services.Contracts;
using DataLayer.Models;
using DataLayer.Repositories.UnitOfWork;

namespace BusinessLayer.Services.Implementations;

public class CategoryService : ICategoryService
{
    private IRepositoriesManager _repositoriesManager;
    private IMapper _mapper;
    public CategoryService(IRepositoriesManager repositoriesManager, IMapper mapper)
    {
        _repositoriesManager = repositoriesManager;
        _mapper = mapper;
    }

    public async Task<Category?> TryGetByNameAsync(string name) =>
        await _repositoriesManager.Categories.TryGetByNameAsync(name);

    public Category? TryGetById(Guid id) =>
         _repositoriesManager.Categories.TryGetById(id);

    public bool Exists(Guid id) => _repositoriesManager.Categories.Exists(id);

    public async Task<IEnumerable<Category>> GetAllAsync() =>
        await _repositoriesManager.Categories.GetAllAsync();

    public async Task<Category> CreateAsync(CategoryDto item)
    {
        var isUniqueName = await _repositoriesManager.Categories.IsUniqueNameAsync(item.Name);
        if (!isUniqueName)
            throw new EntityAlreadyExistException(nameof(Category),"name" ,item.Name);
        var categoryForDb = _mapper.Map<Category>(item);
        await _repositoriesManager.Categories.CreateAsync(categoryForDb);
        await _repositoriesManager.SaveAsync();
        return categoryForDb;
    }

    public void Delete(Guid id)
    {
        var entity = TryGetById(id);
        if (entity == null)
            throw new EntityNotFoundException($"Category with id {id} not found");
        _repositoriesManager.Categories.Delete(entity);
        _repositoriesManager.SaveAsync();
    }
    
}