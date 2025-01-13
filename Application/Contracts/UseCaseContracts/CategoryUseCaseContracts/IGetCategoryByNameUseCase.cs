using Domain.Models;

namespace Application.Contracts.UseCaseContracts.CategoryUseCaseContracts;

public interface IGetCategoryByNameUseCase
{
    Task<Category?> Handle(string name);
}