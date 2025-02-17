
using Interfaces.Common.Exceptions;
using Interfaces.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using OpenPay.Interfaces.Services;
using OpenPay.Interfaces.Services.ServiceModels;

namespace OpenPay.Tests.Api.ItemController;
public class GetByIdAsync
{
    [Fact]
    public async Task GetById_ReturnsStatus400_IfGuidEmpty()
    {
        // Arrange
        IItemService _service = Substitute.For<IItemService>();
        ILogger<ItemsController> _logger = Substitute.For<ILogger<ItemsController>>();
        ItemsController itemsController = new ItemsController(_service, _logger);

        // Act
        ActionResult<ItemResponse> response = await itemsController.GetByIdAsync(Guid.Empty);

        // Assert
        Assert.IsType<BadRequestObjectResult>(response.Result);
        await _service.DidNotReceive().GetByIdAsync(Arg.Any<Guid>());
    }

    [Fact]
    public async Task GetById_ReturnsStatus404_IfNotFound()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        IItemService _service = Substitute.For<IItemService>();
        ILogger<ItemsController> _logger = Substitute.For<ILogger<ItemsController>>();
        _service.GetByIdAsync(id).Returns(Task.FromResult(new Optional<ItemDTO>(new NotFoundException("Item with that Id could not be found."))));
        ItemsController itemsController = new ItemsController(_service, _logger);


        // Act
        ActionResult<ItemResponse> response = await itemsController.GetByIdAsync(id);

        // Assert
        Assert.IsType<NotFoundObjectResult>(response.Result);

        await _service.Received().GetByIdAsync(Arg.Any<Guid>());
    }

    [Fact]
    public async Task GetById_Returns200AndItem_IfFound()
    {
        // Arrange
        IItemService _service = Substitute.For<IItemService>();
        ILogger<ItemsController> _logger = Substitute.For<ILogger<ItemsController>>();
        ItemsController itemsController = new ItemsController(_service, _logger);

        Guid id = Guid.NewGuid();
        _service.GetByIdAsync(id).Returns(Task.FromResult(new Optional<ItemDTO>(new ItemDTO
        {
            Id = id, Name = "test", Price = 10, TaxPercentage = 10
        })));

        // Act
        var response = await itemsController.GetByIdAsync(id);

        // Assert
        var result = Assert.IsType<OkObjectResult>(response.Result);
        var item = Assert.IsType<ItemResponse>(result.Value);

        Assert.Equal(item.Id, id);

        await _service.Received().GetByIdAsync(Arg.Any<Guid>());
    }
}