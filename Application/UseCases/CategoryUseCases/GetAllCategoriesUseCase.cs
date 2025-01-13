using Application.Contracts.RepositoryContracts;
using Application.Contracts.UseCaseContracts.CategoryUseCaseContracts;
using Domain.Models;

namespace Application.UseCases.CategoryUseCases;

public class GetAllCategoriesUseCase : IGetAllCategoriesUseCase
{
    private readonly IRepositoriesManager _repositoriesManager;
    public GetAllCategoriesUseCase(IRepositoriesManager repositoriesManager)
    {
        _repositoriesManager = repositoriesManager;
    }
    public async Task<IEnumerable<Category>> Handle() =>
        await _repositoriesManager.Categories.GetAllAsync();
}