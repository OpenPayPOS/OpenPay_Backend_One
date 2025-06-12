using Interfaces.Common.Exceptions;
using Interfaces.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using OpenPay.Api.V1.Controllers;
using OpenPay.Interfaces.Services;

namespace OpenPay.Tests.Api.Controllers.ItemController;
public class DeleteAsync
{
    [Fact]
    public async Task DeleteAsync_Returns400_IfGuidEmpty()
    {
        // Arrange
        Guid id = Guid.Empty;
        IItemService _service = Substitute.For<IItemService>();
        ILogger<ItemsController> _logger = Substitute.For<ILogger<ItemsController>>();
        ItemsController itemsController = new ItemsController(_service, _logger);

        // Act
        var response = await itemsController.DeleteAsync(id);

        // Assert
        Assert.IsType<BadRequestObjectResult>(response);
        await _service.DidNotReceive().DeleteAsync(Arg.Any<Guid>());
    }

    [Fact]
    public async Task DeleteAsync_Returns404_IfGuidNotFound()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        IItemService _service = Substitute.For<IItemService>();
        ILogger<ItemsController> _logger = Substitute.For<ILogger<ItemsController>>();
        ItemsController itemsController = new ItemsController(_service, _logger);

        _service.DeleteAsync(id).Returns(Task.FromResult(new Optional(new NotFoundException("That id could not get found."))));

        // Act
        var response = await itemsController.DeleteAsync(id);

        // Assert
        await _service.Received().DeleteAsync(Arg.Any<Guid>());
        Assert.IsType<NotFoundObjectResult>(response);
    }

    [Fact]
    public async Task DeleteAsync_Returns204_IfDeleted()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        IItemService _service = Substitute.For<IItemService>();
        ILogger<ItemsController> _logger = Substitute.For<ILogger<ItemsController>>();
        ItemsController itemsController = new ItemsController(_service, _logger);

        _service.DeleteAsync(id).Returns(Task.FromResult(new Optional()));

        // Act
        var response = await itemsController.DeleteAsync(id);

        // Assert
        Assert.IsType<NoContentResult>(response);
        await _service.Received().DeleteAsync(Arg.Any<Guid>());
    }
}