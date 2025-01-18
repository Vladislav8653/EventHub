/*using System;
using System.Threading.Tasks;
using Application.Contracts.RepositoryContracts;
using Application.DtoModels.CategoryDto;
using Application.Exceptions;
using Application.UseCases.CategoryUseCases;
using AutoMapper;
using Domain.Models;
using Moq;
using Xunit;

namespace Tests.CategoriesTest;

public class CategoryUseCasesTest
{
    private readonly Mock<IRepositoriesManager> _mockRepositoriesManager;
    private readonly Mock<IMapper> _mockMapper;
    private readonly CreateCategoryUseCase _useCase;

    public CategoryUseCasesTest()
    {
        _mockRepositoriesManager = new Mock<IRepositoriesManager>();
        _mockMapper = new Mock<IMapper>();
        _useCase = new CreateCategoryUseCase(_mockRepositoriesManager.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task Handle_ShouldCreateCategory_WhenNameIsUnique()
    {
        // Arrange
        var categoryDto = new CategoryDto { Name = "Unique Category" };
        var category = new Category { Name = "Unique Category" };

        _mockRepositoriesManager.Setup(r => r.Categories.IsUniqueNameAsync(categoryDto.Name))
            .ReturnsAsync(true);
        _mockMapper.Setup(m => m.Map<Category>(categoryDto))
            .Returns(category);
        _mockRepositoriesManager.Setup(r => r.Categories.CreateAsync(category))
            .Returns(Task.CompletedTask);
        _mockRepositoriesManager.Setup(r => r.SaveAsync())
            .Returns(Task.CompletedTask);

        // Act
        var result = await _useCase.Handle(categoryDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(category.Name, result.Name);
        _mockRepositoriesManager.Categories.Verify(r => r.CreateAsync(category), Times.Once);
        _mockRepositoriesManager.Verify(r => r.SaveAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowEntityAlreadyExistException_WhenNameIsNotUnique()
    {
        // Arrange
        var categoryDto = new CategoryDto { Name = "Duplicate Category" };

        _mockRepositoriesManager.Setup(r => r.Categories.IsUniqueNameAsync(categoryDto.Name))
            .ReturnsAsync(false);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<EntityAlreadyExistException>(
            () => _useCase.Handle(categoryDto));

        Assert.Equal("Category", exception.EntityType);
        Assert.Equal("name", exception.PropertyName);
        Assert.Equal(categoryDto.Name, exception.PropertyValue);
        _mockRepositoriesManager.Categories.Verify(r => r.CreateAsync(It.IsAny<Category>()), Times.Never);
        _mockRepositoriesManager.Verify(r => r.SaveAsync(), Times.Never);
    }
}*/