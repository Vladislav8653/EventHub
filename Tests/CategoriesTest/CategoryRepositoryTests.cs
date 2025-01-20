using Domain.Models;
using Infrastructure;
using Infrastructure.RepositoryImplementations;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Tests.CategoriesTest;

public class CategoryRepositoryTests
{
    private EventHubDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<EventHubDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        return new EventHubDbContext(options);
    }
    
    [Fact]
    public async Task AddModelToDatabase()
    {
        // Arrange
        var context = CreateContext();
        var repository = new CategoryRepository(context);
        var name = "test_name";
        var cancellationToken = new CancellationToken();
        var model = new Category
        {
            Name = name
        };
        
        //Act
        await repository.CreateAsync(model, cancellationToken);
        await context.SaveChangesAsync();

        // Assert
        var result = await repository.TryGetByNameAsync(name, cancellationToken);
        Assert.NotNull(result);
        Assert.Equal(name, result.Name);
    }
    
    
    [Fact]
    public async Task DeleteModelRemovesCategory()
    {
        // Arrange
        var context = CreateContext();
        var repository = new CategoryRepository(context);
        var name = "test_name";
        var cancellationToken = new CancellationToken();
        var model = new Category
        {
            Name = name
        };
        
        await repository.CreateAsync(model, cancellationToken);
        await context.SaveChangesAsync();

        // Act
        repository.Delete(model); 
        await context.SaveChangesAsync();

        // Assert
        var result = await repository.TryGetByNameAsync(name, cancellationToken);
        Assert.Null(result); 
    }
    
    
    [Fact]
    public async Task GetNonExistentModelByName()
    {
        // Arrange
        var context = CreateContext();
        var cancellationToken = new CancellationToken();
        var repository = new CategoryRepository(context);
    
        // ACt
        var result = await repository.TryGetByNameAsync("non_exist_name", cancellationToken);

        // Assert
        Assert.Null(result); 
    }
}