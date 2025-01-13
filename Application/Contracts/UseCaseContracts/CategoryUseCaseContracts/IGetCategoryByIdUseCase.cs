using Domain.Models;

namespace Application.Contracts.UseCaseContracts.CategoryUseCaseContracts;

public interface IGetCategoryByIdUseCase
{
    Task<Category?> Handle(Guid id);
}