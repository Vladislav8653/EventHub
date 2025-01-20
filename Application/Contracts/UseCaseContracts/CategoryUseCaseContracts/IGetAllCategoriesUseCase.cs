using Domain.Models;

namespace Application.Contracts.UseCaseContracts.CategoryUseCaseContracts;

public interface IGetAllCategoriesUseCase
{
    Task<IEnumerable<Category>> Handle(CancellationToken cancellationToken);
}