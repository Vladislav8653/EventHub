using Application.DtoModels.CategoryDto;
using Domain.Models;

namespace Application.Contracts.UseCaseContracts.CategoryUseCaseContracts;

public interface ICreateCategoryUseCase
{
    Task<Category> Handle(CategoryDto item);
}