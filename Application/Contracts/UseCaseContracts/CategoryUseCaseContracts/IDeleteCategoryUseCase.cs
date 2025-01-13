namespace Application.Contracts.UseCaseContracts.CategoryUseCaseContracts;

public interface IDeleteCategoryUseCase
{
    Task Handle(Guid id);
}