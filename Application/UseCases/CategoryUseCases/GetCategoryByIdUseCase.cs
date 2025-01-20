using Application.Contracts.RepositoryContracts;
using Application.Contracts.UseCaseContracts.CategoryUseCaseContracts;
using Domain.Models;

namespace Application.UseCases.CategoryUseCases;

public class GetCategoryByIdUseCase : IGetCategoryByIdUseCase
{
    private readonly IRepositoriesManager _repositoriesManager;
    public GetCategoryByIdUseCase(IRepositoriesManager repositoriesManager)
    {
        _repositoriesManager = repositoriesManager;
    }
    public async Task<Category?> Handle(Guid id, CancellationToken cancellationToken) =>
        await _repositoriesManager.Categories.TryGetByIdAsync(id, cancellationToken);
}