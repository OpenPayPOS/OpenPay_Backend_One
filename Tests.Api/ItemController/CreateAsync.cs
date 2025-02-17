using Interfaces.Common.Exceptions;
using Interfaces.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using OpenPay.Api.Models.Request;
using OpenPay.Interfaces.Services;
using OpenPay.Interfaces.Services.ServiceModels;
using OpenPay.Tests.Api.Helpers;

namespace OpenPay.Tests.Api.ItemController;
public class CreateAsync
{
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(256)]
    public void CreateAsync_WrongNameLength_ThrowsError(int nameLength)
    {
        // This is done by model validation
        // Arrange
        string name = RandomString.Random(nameLength);
        var model = new CreateItemRequest { Name = name, Price = 1, TaxPercentage = 1 };

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
    public void CreateAsync_WrongPrice_ThrowsError(decimal price)
    {
        // This is done by model validation
        // Arrange
        var model = new CreateItemRequest { Name = RandomString.Random(3, 255), Price = price, TaxPercentage = 1 };

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
    [InlineData(100.001)]
    [InlineData(100.01)]
    [InlineData(101)]
    public void CreateAsync_WrongPercentage_ThrowsError(decimal percentage)
    {
        // This is done by model validation
        // Arrange
        var model = new CreateItemRequest { Name = RandomString.Random(3, 255), Price = 10, TaxPercentage = percentage };

        // Act
        var validationResults = ModelValidationHelper.ValidateModel(model);

        // Assert
        Assert.NotEmpty(validationResults);
    }

    [Fact]
    public async Task CreateAsync_Returns201AndItem_IfCreated()
    {
        // Arrange
        IItemService _service = Substitute.For<IItemService>();
        ILogger<ItemsController> _logger = Substitute.For<ILogger<ItemsController>>();
        ItemsController itemsController = new ItemsController(_service, _logger);

        var model = new CreateItemRequest { Name = RandomString.Random(3, 255), Price = 10, TaxPercentage = 1 };

        _service.CreateAsync(model.Name, model.Price, model.TaxPercentage)
            .Returns(Task.FromResult(new Optional<ItemDTO>(new ItemDTO
                {
                    Id = Guid.NewGuid(),
                    Name = model.Name,
                    Price = model.Price,
                    TaxPercentage = model.TaxPercentage
                })));

        // Act
        var result = await itemsController.CreateAsync(model);

        // Assert
        await _service.Received(1).CreateAsync(Arg.Any<string>(), Arg.Any<decimal>(), Arg.Any<decimal>());
        Assert.IsType<CreatedAtActionResult>(result.Result);
    }

    [Fact]
    public async Task CreateAsync_Returns400_IfNameExists()
    {
        // Arrange
        IItemService _service = Substitute.For<IItemService>();
        ILogger<ItemsController> _logger = Substitute.For<ILogger<ItemsController>>();
        ItemsController itemsController = new ItemsController(_service, _logger);

        string name = RandomString.Random(3, 255);
        var model = new CreateItemRequest { Name = name, Price = 10, TaxPercentage = 1 };

        _service.CreateAsync(name, model.Price, model.TaxPercentage).Returns(new Optional<ItemDTO>(new BadRequestException("Name already exists")));

        // Act
        var result = await itemsController.CreateAsync(model);

        // Assert
        await _service.Received().CreateAsync(Arg.Any<string>(), Arg.Any<decimal>(), Arg.Any<decimal>());
        Assert.IsType<BadRequestObjectResult>(result.Result);
    }
}