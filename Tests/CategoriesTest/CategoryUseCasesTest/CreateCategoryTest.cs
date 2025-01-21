using Domain.RepositoryContracts;
using Application.DtoModels.CategoryDto;
using Application.Exceptions;
using Application.UseCases.CategoryUseCases;
using AutoMapper;
using Domain;
using Domain.Models;
using Domain.RepositoryContracts;
using Moq;
using Xunit;

namespace Tests.CategoriesTest.CategoryUseCasesTest;

public class CreateCategoryTest
{
    private readonly Mock<IRepositoriesManager> _mockRepositoriesManager;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<ICategoryRepository> _mockCategoriesRepository;
    private readonly CreateCategoryUseCase _useCase;

    public CreateCategoryTest()
    {
        _mockRepositoriesManager = new Mock<IRepositoriesManager>();
        _mockMapper = new Mock<IMapper>();
        
        _mockCategoriesRepository = new Mock<ICategoryRepository>(); 
        _mockRepositoriesManager.Setup(r => r.Categories).Returns(_mockCategoriesRepository.Object);
        
        _useCase = new CreateCategoryUseCase(_mockRepositoriesManager.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task Handle_ShouldCreateCategory_WhenNameIsUnique()
    {
        // Arrange
        var categoryDto = new CategoryDto { Name = "Unique Category" };
        var category = new Category { Name = "Unique Category" };
        var cancellationToken = new CancellationToken();

        _mockCategoriesRepository.Setup(r => r.IsUniqueNameAsync(categoryDto.Name, cancellationToken))
            .ReturnsAsync(true);
        _mockMapper.Setup(m => m.Map<Category>(categoryDto))
            .Returns(category);
        _mockCategoriesRepository.Setup(r => r.CreateAsync(category, cancellationToken))
            .Returns(Task.CompletedTask);
        _mockRepositoriesManager.Setup(r => r.SaveAsync())
            .Returns(Task.CompletedTask);

        // Act
        var result = await _useCase.Handle(categoryDto, cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(category.Name, result.Name);
        _mockCategoriesRepository.Verify(r => r.CreateAsync(category, cancellationToken), Times.Once);
        _mockRepositoriesManager.Verify(r => r.SaveAsync(), Times.Once);
    }
    

    [Fact]
    public async Task Handle_ShouldThrowEntityAlreadyExistException_WhenNameIsNotUnique()
    {
        // Arrange
        var categoryDto = new CategoryDto { Name = "Duplicate Category" };
        var cancellationToken = new CancellationToken(); 
        _mockCategoriesRepository.Setup(r => r.IsUniqueNameAsync(categoryDto.Name, cancellationToken))
            .ReturnsAsync(false);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<EntityAlreadyExistException>(
            () => _useCase.Handle(categoryDto, cancellationToken)); 
        
        Assert.NotNull(exception);
        
        _mockCategoriesRepository.Verify(r => r.CreateAsync(It.IsAny<Category>(), cancellationToken), Times.Never);
        _mockRepositoriesManager.Verify(r => r.SaveAsync(), Times.Never);
    }
}