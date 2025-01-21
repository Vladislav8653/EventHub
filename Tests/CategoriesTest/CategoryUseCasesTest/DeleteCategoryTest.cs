using Domain.RepositoryContracts;
using Application.Exceptions;
using Application.UseCases.CategoryUseCases;
using Domain.Models;
using Domain.RepositoryContracts;
using Moq;
using Xunit;

namespace Tests.CategoriesTest.CategoryUseCasesTest;

public class DeleteCategoryTest
{
    private readonly Mock<IRepositoriesManager> _mockRepositoriesManager;
    private readonly Mock<ICategoryRepository> _mockCategoriesRepository;
    private readonly DeleteCategoryUseCase _useCase;

    public DeleteCategoryTest()
    {
        _mockRepositoriesManager = new Mock<IRepositoriesManager>();
        _mockCategoriesRepository = new Mock<ICategoryRepository>(); 

        _mockRepositoriesManager.Setup(r => r.Categories).Returns(_mockCategoriesRepository.Object);
        
        _useCase = new DeleteCategoryUseCase(_mockRepositoriesManager.Object);
    }

    [Fact]
    public async Task Handle_ShouldDeleteCategory_WhenCategoryExists()
    {
        // Arrange
        var categoryId = Guid.NewGuid(); 
        var cancellationToken = new CancellationToken();
        var category = new Category { Id = categoryId }; 

        _mockCategoriesRepository.Setup(r => r.TryGetByIdAsync(categoryId, cancellationToken))
            .ReturnsAsync(category);
        _mockCategoriesRepository.Setup(r => r.Delete(category)); 
        _mockRepositoriesManager.Setup(r => r.SaveAsync())
            .Returns(Task.CompletedTask);

        // Act
        await _useCase.Handle(categoryId, cancellationToken);

        // Assert
        _mockCategoriesRepository.Verify(r => r.TryGetByIdAsync(categoryId, cancellationToken), Times.Once);
        _mockCategoriesRepository.Verify(r => r.Delete(category), Times.Once);
        _mockRepositoriesManager.Verify(r => r.SaveAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowEntityNotFoundException_WhenCategoryDoesNotExist()
    {
        // Arrange
        var categoryId = Guid.NewGuid();
        var cancellationToken = new CancellationToken();

        _mockCategoriesRepository.Setup(r => r.TryGetByIdAsync(categoryId, cancellationToken))
            .ReturnsAsync(null as Category); 

        // Act & Assert
        var exception = await Assert.ThrowsAsync<EntityNotFoundException>(
            () => _useCase.Handle(categoryId, cancellationToken));
        
        Assert.NotNull(exception);
        
        _mockCategoriesRepository.Verify(r => r.Delete(It.IsAny<Category>()), Times.Never);
        _mockRepositoriesManager.Verify(r => r.SaveAsync(), Times.Never);
    }
}