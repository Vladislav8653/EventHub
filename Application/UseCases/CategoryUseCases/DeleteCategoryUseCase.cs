using Domain.RepositoryContracts;
using Application.Contracts.UseCaseContracts.CategoryUseCaseContracts;
using Application.Exceptions;
using Domain;

namespace Application.UseCases.CategoryUseCases;

public class DeleteCategoryUseCase : IDeleteCategoryUseCase
{
    private readonly IRepositoriesManager _repositoriesManager;
    public DeleteCategoryUseCase(IRepositoriesManager repositoriesManager)
    {
        _repositoriesManager = repositoriesManager;
    }
    public async Task Handle(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _repositoriesManager.Categories.TryGetByIdAsync(id, cancellationToken);
        if (entity == null)
            throw new EntityNotFoundException($"Category with id {id} not found");
        _repositoriesManager.Categories.Delete(entity);
        await _repositoriesManager.SaveAsync();
    }
}