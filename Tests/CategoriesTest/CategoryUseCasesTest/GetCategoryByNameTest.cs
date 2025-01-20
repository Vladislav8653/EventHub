using Application.Contracts.RepositoryContracts;
using Application.UseCases.CategoryUseCases;
using Domain.Models;
using Moq;
using Xunit;

namespace Tests.CategoriesTest.CategoryUseCasesTest;

public class GetCategoryByNameTest
{
    private readonly Mock<ICategoryRepository> _mockCategoriesRepository;
    private readonly GetCategoryByNameUseCase _useCase;

    public GetCategoryByNameTest()
    {
        Mock<IRepositoriesManager> mockRepositoriesManager = new();
        _mockCategoriesRepository = new Mock<ICategoryRepository>();

        mockRepositoriesManager.Setup(r => r.Categories).Returns(_mockCategoriesRepository.Object);
        
        _useCase = new GetCategoryByNameUseCase(mockRepositoriesManager.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnCategory_WhenCategoryExists()
    {
        // Arrange
        var categoryName = "Existing Category"; 
        var cancellationToken = new CancellationToken();
        var category = new Category { Id = Guid.NewGuid(), Name = categoryName };

        _mockCategoriesRepository.Setup(r => r.TryGetByNameAsync(categoryName, cancellationToken))
            .ReturnsAsync(category);

        // Act
        var result = await _useCase.Handle(categoryName, cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(categoryName, result.Name); 
        _mockCategoriesRepository.Verify(r => r.TryGetByNameAsync(categoryName, cancellationToken), Times.Once); // Убедитесь, что метод был вызван
    }

    [Fact]
    public async Task Handle_ShouldReturnNull_WhenCategoryDoesNotExist()
    {
        // Arrange
        var categoryName = "Nonexistent Category"; 
        var cancellationToken = new CancellationToken();

        _mockCategoriesRepository.Setup(r => r.TryGetByNameAsync(categoryName, cancellationToken))
            .ReturnsAsync(null as Category); 

        // Act
        var result = await _useCase.Handle(categoryName, cancellationToken);

        // Assert
        Assert.Null(result); 
        _mockCategoriesRepository.Verify(r => r.TryGetByNameAsync(categoryName, cancellationToken), Times.Once); // Убедитесь, что метод был вызван
    }
}