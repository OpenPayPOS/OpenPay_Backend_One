using Interfaces.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using NSubstitute.ExceptionExtensions;
using OpenPay.Data.DataModels;

namespace Tests.Data.ItemRepositoryTests;
public class GetByIdAsync
{
    [Fact]
    public async Task GetByIdAsync_Returns_Item_When_Exists()
    {
        // Mock DbContext and Logger
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("TestDb")
            .Options;
        var _dbContext = new AppDbContext(options);
        ILogger<ItemRepository> _logger = Substitute.For<ILogger<ItemRepository>>();

        // Initialize the repository with mocked dependencies

        ItemRepository _repository = new ItemRepository(_dbContext, _logger);
        // Arrange
        var itemId = Guid.NewGuid();
        var item = new ItemDataModel { Id = itemId, Name = "Item1", Price = 10.5m, TaxPercentage = 5m };

        _dbContext.Items.Add(item);

        // Act
        var result = await _repository.GetByIdAsync(itemId);

        // Assert
        result.Handle(value =>
        {
            Assert.Equal(itemId, value.Id);
        }, exception =>
        {
            Assert.Fail();
        });
    }

    [Fact]
    public async Task GetByIdAsync_Returns_NotFoundException_When_Item_Does_Not_Exist()
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
        var result = await repository.GetByIdAsync(itemId);

        // Assert
        result.Handle(value =>
        {
            Assert.Fail();
        }, error =>
        {
            Assert.IsType<NotFoundException>(error);
        });
    }
}