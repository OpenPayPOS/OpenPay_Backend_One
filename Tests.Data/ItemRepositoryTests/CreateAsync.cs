using Microsoft.EntityFrameworkCore;
using OpenPay.Interfaces.Data.DataModels.Item;

namespace Tests.Data.ItemRepositoryTests;
public class CreateAsync
{
    [Fact]
    public async Task CreateAsync_Creates_And_Returns_New_Item()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("TestDb")
            .Options;
        var dbContext = new AppDbContext(options);
        var logger = Substitute.For<ILogger<ItemRepository>>();
        var repository = new ItemRepository(dbContext, logger);

        var itemDto = new ItemDataDTO { Id = Guid.NewGuid(), Name = "Item1", Price = 10.5m, TaxPercentage = 5m };

        // Act
        var result = await repository.CreateAsync(itemDto);

        // Assert
        result.Handle(value =>
        {
            Assert.Equal(itemDto.Id, value.Id);
        }, error =>
        {
            Assert.Fail();
        });
    }
}