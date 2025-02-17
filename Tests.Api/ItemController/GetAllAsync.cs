using Microsoft.Extensions.Logging;
using NSubstitute;
using OpenPay.Interfaces.Services;
using OpenPay.Interfaces.Services.ServiceModels;

namespace OpenPay.Tests.Api.ItemController;
public class GetAllAsync
{
    [Fact]
    public async Task GetAllAsync_ReturnsEmptyEnumerable_IfEmpty()
    {
        // Arrange
        IItemService _service = Substitute.For<IItemService>();
        ILogger<ItemsController> _logger = Substitute.For<ILogger<ItemsController>>();
        ItemsController itemsController = new ItemsController(_service, _logger);

        _service.GetAllAsync().Returns(Array.Empty<ItemDTO>().ToAsyncEnumerable());
        
        List<ItemResponse> items = [];

        // Act
        await foreach (var item in itemsController.GetAllAsync())
        {
            items.Add(item);
        }

        // Assert
        _service.Received().GetAllAsync();
        Assert.Empty(items);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAll_IfAny()
    {
        // Arrange
        IItemService _service = Substitute.For<IItemService>();
        ILogger<ItemsController> _logger = Substitute.For<ILogger<ItemsController>>();
        ItemsController itemsController = new ItemsController(_service, _logger);

        _service.GetAllAsync().Returns(new List<ItemDTO> { new ItemDTO { Id = Guid.NewGuid(), Name = "test", Price = 10, TaxPercentage = 10 } }.ToAsyncEnumerable());

        List<ItemResponse> items = [];
        // TODO: Add data return in service mock

        // Act
        await foreach (var item in itemsController.GetAllAsync())
        {
            items.Add(item);
        }

        // Assert
        _service.Received().GetAllAsync();
        Assert.NotEmpty(items);
    }
}