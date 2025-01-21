using Domain.RepositoryContracts;
using Application.UseCases.CategoryUseCases;
using Domain;
using Domain.Models;
using Domain.RepositoryContracts;
using Moq;
using Xunit;

namespace Tests.CategoriesTest.CategoryUseCasesTest;

public class GetCategoryByIdTest
{
    private readonly Mock<ICategoryRepository> _mockCategoriesRepository;
    private readonly GetCategoryByIdUseCase _useCase;

    public GetCategoryByIdTest()
    {
        Mock<IRepositoriesManager> mockRepositoriesManager = new();
        _mockCategoriesRepository = new Mock<ICategoryRepository>();

        mockRepositoriesManager.Setup(r => r.Categories).Returns(_mockCategoriesRepository.Object);
        
        _useCase = new GetCategoryByIdUseCase(mockRepositoriesManager.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnCategory_WhenCategoryExists()
    {
        // Arrange
        var categoryId = Guid.NewGuid(); 
        var cancellationToken = new CancellationToken();
        var category = new Category { Id = categoryId, Name = "Existing Category" };

        _mockCategoriesRepository.Setup(r => r.TryGetByIdAsync(categoryId, cancellationToken))
            .ReturnsAsync(category);

        // Act
        var result = await _useCase.Handle(categoryId, cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(categoryId, result.Id); 
        Assert.Equal(category.Name, result.Name);
        _mockCategoriesRepository.Verify(r => r.TryGetByIdAsync(categoryId, cancellationToken), Times.Once); // Убедитесь, что метод был вызван
    }

    [Fact]
    public async Task Handle_ShouldReturnNull_WhenCategoryDoesNotExist()
    {
        // Arrange
        var categoryId = Guid.NewGuid(); 
        var cancellationToken = new CancellationToken();

        _mockCategoriesRepository.Setup(r => r.TryGetByIdAsync(categoryId, cancellationToken))
            .ReturnsAsync(null as Category); 

        // Act
        var result = await _useCase.Handle(categoryId, cancellationToken);

        // Assert
        Assert.Null(result); 
        _mockCategoriesRepository.Verify(r => r.TryGetByIdAsync(categoryId, cancellationToken), Times.Once); // Убедитесь, что метод был вызван
    }
}