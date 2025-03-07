using Microsoft.EntityFrameworkCore;
using OpenPay.Interfaces.Data.DataModels.Item;

namespace Tests.Data.ItemRepositoryTests;
public class EditAsync
{
    [Fact]
    public async Task EditAsync_Edits_And_Returns_Updated_Item()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("TestDb")
            .Options;
        var dbContext = new AppDbContext(options);
        var logger = Substitute.For<ILogger<ItemRepository>>();
        var repository = new ItemRepository(dbContext, logger);

        var itemId = Guid.NewGuid();
        var item = new ItemDataModel { Id = itemId, Name = "OldName", Price = 10m, TaxPercentage = 5m };
        var editDto = new EditItemDataDTO { Name = "NewName", Price = 15m };

        dbContext.Items.Add(item);

        // Act
        var result = await repository.EditAsync(itemId, editDto);

        // Assert
        result.Handle(value =>
        {
            Assert.Equal("NewName", value.Name);
            Assert.Equal(15m, value.Price);
        }, error =>
        {
            Assert.Fail();
        });
    }
}