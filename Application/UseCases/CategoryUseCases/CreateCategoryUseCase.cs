using Application.Contracts.RepositoryContracts;
using Application.Contracts.UseCaseContracts.CategoryUseCaseContracts;
using Application.DtoModels.CategoryDto;
using Application.Exceptions;
using AutoMapper;
using Domain.Models;

namespace Application.UseCases.CategoryUseCases;

public class CreateCategoryUseCase : ICreateCategoryUseCase
{
    private readonly IRepositoriesManager _repositoriesManager;
    private readonly IMapper _mapper;
    public CreateCategoryUseCase(IRepositoriesManager repository, IMapper mapper)
    {
        _repositoriesManager = repository;
        _mapper = mapper;
    }
    public async Task<Category> Handle(CategoryDto item)
    {
        var isUniqueName = await _repositoriesManager.Categories.IsUniqueNameAsync(item.Name);
        if (!isUniqueName)
            throw new EntityAlreadyExistException(nameof(Category),"name" ,item.Name);
        var categoryForDb = _mapper.Map<Category>(item);
        await _repositoriesManager.Categories.CreateAsync(categoryForDb);
        await _repositoriesManager.SaveAsync();
        return categoryForDb;
    }
}