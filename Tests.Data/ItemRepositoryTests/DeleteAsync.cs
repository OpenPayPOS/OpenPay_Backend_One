using Interfaces.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using NSubstitute.ExceptionExtensions;

namespace Tests.Data.ItemRepositoryTests;
public class DeleteAsync
{
    [Fact]
    public async Task DeleteAsync_Removes_Item_When_Exists()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("TestDb")
            .Options;
        var dbContext = new AppDbContext(options);
        var logger = Substitute.For<ILogger<ItemRepository>>();
        var repository = new ItemRepository(dbContext, logger);

        var itemId = Guid.NewGuid();
        var item = new ItemDataModel { Id = itemId, Name = "Item1", Price = 10m, TaxPercentage = 5m };

        dbContext.Items.Add(item);

        // Act
        var result = await repository.DeleteAsync(itemId);

        // Assert
        result.Handle(_ =>
        {
        }, error =>
        {
            Assert.Fail();
        });
    }

    [Fact]
    public async Task DeleteAsync_Returns_NotFoundException_When_Item_Does_Not_Exist()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("TestDb")
            .Options;
        var dbContext = new AppDbContext(options);
        var logger = Substitute.For<ILogger<ItemRepository>>();
        var repository = new ItemRepository(dbContext, logger);

        var itemId = Guid.NewGuid();

        // Act
        var result = await repository.DeleteAsync(itemId);

        // Assert
        result.Handle(_ =>
        {
            Assert.Fail();
        }, _ =>
        {
        });
    }
}