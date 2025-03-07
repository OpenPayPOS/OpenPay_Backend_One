using Interfaces.Common.Exceptions;
using Interfaces.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using OpenPay.Api.Models.Request;
using OpenPay.Interfaces.Services.ServiceModels;
using OpenPay.Interfaces.Services;
using OpenPay.Tests.Api.Helpers;

namespace OpenPay.Tests.Api.Controllers.OrderController;
public class CreateAsync
{
    [Fact]
    public void CreateAsync_EmptyList_ThrowsError()
    {
        // This is done by model validation
        // Arrange
        var model = new CreateOrderRequest { OrderItems = new List<CreateOrderItem>() };

        // Act
        var validationResults = ModelValidationHelper.ValidateModel(model);

        // Assert
        Assert.NotEmpty(validationResults);
    }

    [Theory]
    [InlineData(-0.001)]
    [InlineData(-0.01)]
    [InlineData(-1)]
    [InlineData(int.MinValue)]
    [InlineData(0.001)]
    [InlineData(10.001)]
    public void CreateAsync_WrongAmount_ThrowsError(decimal amount)
    {
        // This is done by model validation
        // Arrange
        var model = new CreateOrderItem { ItemId = Guid.NewGuid(), Amount = amount };

        // Act
        var validationResults = ModelValidationHelper.ValidateModel(model);

        // Assert
        Assert.NotEmpty(validationResults);
    }

    [Fact]
    public async Task CreateAsync_Returns201AndOrder_IfCreated()
    {
        // Arrange
        IOrderService _service = Substitute.For<IOrderService>();
        ILogger<OrdersController> _logger = Substitute.For<ILogger<OrdersController>>();
        OrdersController ordersController = new OrdersController(_service, _logger);

        Guid itemId = Guid.NewGuid();

        var model = new CreateOrderRequest
        {
            OrderItems = new List<CreateOrderItem> {
            new CreateOrderItem
            {
                Amount = 1,
                ItemId = itemId
            }
        }
        };

        _service.IdExistsAsync(itemId)
            .Returns(Task.FromResult(new Optional<bool>(true)));

        _service.CreateAsync(Arg.Any<List<CreateOrderItemDTO>>())
            .Returns(Task.FromResult(new Optional<OrderDTO>(
                new OrderDTO
                {
                    Id = Guid.NewGuid(),
                    OrderItems = new List<OrderItemDTO>
                    {
                        new OrderItemDTO
                        {
                            Amount = 1,
                            Item = new ItemDTO
                            {
                                Id = itemId,
                                Name = "Name",
                                Price = 10,
                                TaxPercentage = 10
                            }
                        }
                    }
                }
                )));


        // Act
        var result = await ordersController.CreateAsync(model);

        // Assert
        await _service.Received(1).CreateAsync(Arg.Any<List<CreateOrderItemDTO>>());
        Assert.IsType<CreatedAtActionResult>(result.Result);
    }
}