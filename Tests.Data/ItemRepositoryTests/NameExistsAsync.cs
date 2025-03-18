using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using NSubstitute.ExceptionExtensions;
using System.Linq.Expressions;

namespace Tests.Data.ItemRepositoryTests;
public class NameExistsAsync
{

    [Fact]
    public async Task NameExistsAsync_Returns_False_When_Item_Does_Not_Exist()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("TestDb")
            .Options;
        var dbContext = new AppDbContext(options);
        var logger = Substitute.For<ILogger<ItemRepository>>();
        var repository = new ItemRepository(dbContext, logger);

        var name = "NonExistingItem";
        dbContext.Items.Add(new ItemDataModel { Id = Guid.NewGuid(), Name = "ExistingItem", Price = 10m, TaxPercentage = 5m , ImagePath = "test" });
        await dbContext.SaveChangesAsync();

        // Act
        var result = await repository.NameExistsAsync(name);

        // Assert
        result.Handle(v =>
        {
            Assert.False(v);
        }, _ => Assert.Fail());
    }

    [Fact]
    public async Task NameExistsAsync_Returns_True_When_Item_Does_Exist()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("TestDb")
            .Options;
        var dbContext = new AppDbContext(options);
        var logger = Substitute.For<ILogger<ItemRepository>>();
        var repository = new ItemRepository(dbContext, logger);

        var name = "ExistingItem";
        dbContext.Items.Add(new ItemDataModel { Id = Guid.NewGuid(), Name = "ExistingItem", Price = 10m, TaxPercentage = 5m , ImagePath = "test" });
        await dbContext.SaveChangesAsync();

        // Act
        var result = await repository.NameExistsAsync(name);

        // Assert
        result.Handle(v =>
        {
            Assert.True(v);
        }, _ => Assert.Fail());
    }
}