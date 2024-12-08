using DataLayer.Data;
using DataLayer.Models;
using DataLayer.Repositories.RepositoriesImplementations;
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
        var model = new Category
        {
            Name = name
        };
        
        //Act
        await repository.CreateAsync(model);
        await context.SaveChangesAsync();

        // Assert
        var result = await repository.TryGetByNameAsync(name);
        Assert.NotNull(result);
        Assert.Equal(name, result.Name);
    }
    
    [Fact]
    public async Task GetNonExistentModelByNameReturnsNull()
    {
        // Arrange
        var context = CreateContext();
        var repository = new CategoryRepository(context);
    
        // ACt
        var result = await repository.TryGetByNameAsync("non_exist_name");

        // Assert
        Assert.Null(result); // Проверяем, что результат равен null
    }
    
}