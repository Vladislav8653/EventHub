using Application.Contracts.RepositoryContracts;
using Application.Contracts.UseCaseContracts.CategoryUseCaseContracts;
using Domain.Models;

namespace Application.UseCases.CategoryUseCases;

public class GetCategoryByNameUseCase : IGetCategoryByNameUseCase
{
    private readonly IRepositoriesManager _repositoriesManager;
    public GetCategoryByNameUseCase(IRepositoriesManager repositoriesManager)
    {
        _repositoriesManager = repositoriesManager;
    }
    public async Task<Category?> Handle(string name) =>
        await _repositoriesManager.Categories.TryGetByNameAsync(name);
}