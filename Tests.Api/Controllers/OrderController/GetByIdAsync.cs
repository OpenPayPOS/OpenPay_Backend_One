using Interfaces.Common.Exceptions;
using Interfaces.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using OpenPay.Interfaces.Services.ServiceModels;
using OpenPay.Interfaces.Services;
using OpenPay.Api.V1.Controllers;
using OpenPay.Api.V1.Models.Response;

namespace OpenPay.Tests.Api.Controllers.OrderController;
public class GetByIdAsync
{
    [Fact]
    public async Task GetById_ReturnsStatus400_IfGuidEmpty()
    {
        // Arrange
        IOrderService _service = Substitute.For<IOrderService>();
        ILogger<OrdersController> _logger = Substitute.For<ILogger<OrdersController>>();
        OrdersController ordersController = new OrdersController(_service, _logger);

        // Act
        ActionResult<OrderResponse> response = await ordersController.GetByIdAsync(Guid.Empty);

        // Assert
        Assert.IsType<BadRequestObjectResult>(response.Result);
        await _service.DidNotReceive().GetByIdAsync(Arg.Any<Guid>());
    }

    [Fact]
    public async Task GetById_ReturnsStatus404_IfNotFound()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        IOrderService _service = Substitute.For<IOrderService>();
        ILogger<OrdersController> _logger = Substitute.For<ILogger<OrdersController>>();
        _service.GetByIdAsync(id).Returns(Task.FromResult(new Optional<OrderDTO>(new NotFoundException("Item with that Id could not be found."))));
        OrdersController ordersController = new OrdersController(_service, _logger);


        // Act
        ActionResult<OrderResponse> response = await ordersController.GetByIdAsync(id);

        // Assert
        Assert.IsType<NotFoundObjectResult>(response.Result);

        await _service.Received().GetByIdAsync(Arg.Any<Guid>());
    }

    [Fact]
    public async Task GetById_Returns200AndItem_IfFound()
    {
        // Arrange
        IOrderService _service = Substitute.For<IOrderService>();
        ILogger<OrdersController> _logger = Substitute.For<ILogger<OrdersController>>();
        OrdersController ordersController = new OrdersController(_service, _logger);

        Guid id = Guid.NewGuid();
        _service.GetByIdAsync(id).Returns(Task.FromResult(new Optional<OrderDTO>(new OrderDTO
        {
            Id = id,
            OrderItems = new List<OrderItemDTO>
            {
                new OrderItemDTO
                {
                    Amount = 1,
                    Item = new ItemDTO
                    {
                        Id = Guid.NewGuid(),
                        Name = "test item",
                        Price = 10,
                        TaxPercentage = 10
                    }
                }
            }
        })));

        // Act
        var response = await ordersController.GetByIdAsync(id);

        // Assert
        var result = Assert.IsType<OkObjectResult>(response.Result);
        var order = Assert.IsType<OrderResponse>(result.Value);

        Assert.Equal(order.OrderItems?.Count, 1);

        await _service.Received().GetByIdAsync(Arg.Any<Guid>());
    }
}