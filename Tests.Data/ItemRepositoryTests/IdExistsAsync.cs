using Microsoft.EntityFrameworkCore;
using NSubstitute.ExceptionExtensions;

namespace Tests.Data.ItemRepositoryTests;
public class IdExistsAsync
{

    [Fact]
    public async Task IdExistsAsync_Returns_True_When_Item_Exists()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("TestDb")
            .Options;
        var dbContext = new AppDbContext(options);
        var logger = Substitute.For<ILogger<ItemRepository>>();
        var repository = new ItemRepository(dbContext, logger);

        var itemId = Guid.NewGuid();
        await dbContext.Items.AddAsync(new ItemDataModel { Id = itemId, Name = "ExistingItem", Price = 10m, TaxPercentage = 5m, ImagePath = "test" });
        await dbContext.SaveChangesAsync();

        // Act
        var result = await repository.IdExistsAsync(itemId);

        // Assert
        result.Handle(value =>
        {
            Assert.True(value);
        }, error =>
        {
            Assert.Fail();
        });
    }

    [Fact]
    public async Task IdExistsAsync_Returns_False_When_Item_DoesNotExists()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("TestDb")
            .Options;
        var dbContext = new AppDbContext(options);
        var logger = Substitute.For<ILogger<ItemRepository>>();
        var repository = new ItemRepository(dbContext, logger);

        var itemId = Guid.NewGuid();
        dbContext.Items.Add(new ItemDataModel { Id = itemId, Name = "ExistingItem", Price = 10m, TaxPercentage = 5m });

        // Act
        var result = await repository.IdExistsAsync(Guid.NewGuid());

        // Assert
        result.Handle(value =>
        {
            Assert.False(value);
        }, error =>
        {
            Assert.Fail();
        });
    }
}