using Domain.RepositoryContracts;
using Application.Contracts.UseCaseContracts.CategoryUseCaseContracts;
using Domain;
using Domain.Models;

namespace Application.UseCases.CategoryUseCases;

public class GetCategoryByNameUseCase : IGetCategoryByNameUseCase
{
    private readonly IRepositoriesManager _repositoriesManager;
    public GetCategoryByNameUseCase(IRepositoriesManager repositoriesManager)
    {
        _repositoriesManager = repositoriesManager;
    }
    public async Task<Category?> Handle(string name, CancellationToken cancellationToken) =>
        await _repositoriesManager.Categories.TryGetByNameAsync(name, cancellationToken);
}