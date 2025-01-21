using Domain.RepositoryContracts;
using Application.Contracts.UseCaseContracts.CategoryUseCaseContracts;
using Domain;
using Domain.Models;

namespace Application.UseCases.CategoryUseCases;

public class GetAllCategoriesUseCase : IGetAllCategoriesUseCase
{
    private readonly IRepositoriesManager _repositoriesManager;
    public GetAllCategoriesUseCase(IRepositoriesManager repositoriesManager)
    {
        _repositoriesManager = repositoriesManager;
    }
    public async Task<IEnumerable<Category>> Handle(CancellationToken cancellationToken) =>
        await _repositoriesManager.Categories.GetAllAsync(cancellationToken);
}