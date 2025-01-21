using Domain.RepositoryContracts;
using Application.UseCases.CategoryUseCases;
using Domain.Models;
using Domain.RepositoryContracts;
using Moq;
using Xunit;

namespace Tests.CategoriesTest.CategoryUseCasesTest;

public class GetAllCategoriesTest
{
    private readonly Mock<ICategoryRepository> _mockCategoriesRepository;
    private readonly GetAllCategoriesUseCase _useCase;

    public GetAllCategoriesTest()
    {
        Mock<IRepositoriesManager> mockRepositoriesManager = new();
        _mockCategoriesRepository = new Mock<ICategoryRepository>();

        mockRepositoriesManager.Setup(r => r.Categories).Returns(_mockCategoriesRepository.Object);
        
        _useCase = new GetAllCategoriesUseCase(mockRepositoriesManager.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnAllCategories()
    {
        // Arrange
        var cancellationToken = new CancellationToken();
        var categories = new List<Category>
        {
            new Category { Id = Guid.NewGuid(), Name = "Category 1" },
            new Category { Id = Guid.NewGuid(), Name = "Category 2" }
        };

        _mockCategoriesRepository.Setup(r => r.GetAllAsync(cancellationToken))
            .ReturnsAsync(categories);

        // Act
        var result = await _useCase.Handle(cancellationToken);

        // Assert
        Assert.NotNull(result);
        var enumerable = result.ToList();
        Assert.Equal(2, enumerable.Count); 
        Assert.Equal(categories, enumerable); 
        _mockCategoriesRepository.Verify(r => r.GetAllAsync(cancellationToken), Times.Once);    }
}