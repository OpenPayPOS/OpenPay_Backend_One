using Interfaces.Common.Exceptions;
using Interfaces.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using OpenPay.Api.Models.Request;
using OpenPay.Interfaces.Services;
using OpenPay.Interfaces.Services.ServiceModels;
using OpenPay.Tests.Api.Helpers;

namespace OpenPay.Tests.Api.Controllers.ItemController;
public class EditAsync
{
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(256)]
    public void EditAsync_WrongNameLength_ThrowsError(int nameLength)
    {
        // This is done by model validation
        // Arrange
        Guid id = Guid.NewGuid();
        string name = RandomString.Random(nameLength);
        var model = new EditItemRequest { Id = id, Name = name };

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
    public void EditAsync_WrongPrice_ThrowsError(int price)
    {
        // This is done by model validation
        // Arrange
        Guid id = Guid.NewGuid();
        var model = new EditItemRequest { Id = id, Price = price };

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
    public void EditAsync_WrongPercentage_ThrowsError(int percentage)
    {
        // This is done by model validation
        // Arrange
        Guid id = Guid.NewGuid();
        var model = new EditItemRequest { Id = id, TaxPercentage = percentage };

        // Act
        var validationResults = ModelValidationHelper.ValidateModel(model);

        // Assert
        Assert.NotEmpty(validationResults);
    }

    [Theory]
    [InlineData(false, null, null)]
    [InlineData(true, null, null)]
    [InlineData(false, 1.0, null)]
    [InlineData(false, null, 1.0)]
    [InlineData(true, 1.0, 1.0)]
    public async Task EditAsync_Returns201AndItem_IfEdited(bool generateName, double? priceDouble, double? taxPercentageDouble)
    {
        // Arrange
        decimal? price = null;
        decimal? taxPercentage = null;
        if (priceDouble != null) price = Convert.ToDecimal(priceDouble);
        if (taxPercentageDouble != null) taxPercentage = Convert.ToDecimal(taxPercentageDouble);
        IItemService _service = Substitute.For<IItemService>();
        ILogger<ItemsController> _logger = Substitute.For<ILogger<ItemsController>>();
        ItemsController itemsController = new ItemsController(_service, _logger);

        string? name = generateName ? RandomString.Random(3, 255) : null;
        Guid id = Guid.NewGuid();

        var model = new EditItemRequest { Id = id, Name = name, Price = price, TaxPercentage = taxPercentage };

        _service.EditAsync(id, name, price, taxPercentage)
            .Returns(Task.FromResult(new Optional<ItemDTO>(new ItemDTO
            {
                Id = id,
                Name = name ?? "old_name",
                Price = price ?? 10,
                TaxPercentage = taxPercentage ?? 10

            })));

        // Act
        var response = await itemsController.EditAsync(model);

        // Assert
        await _service.Received().EditAsync(Arg.Any<Guid>(), Arg.Any<string?>(), Arg.Any<decimal?>(), Arg.Any<decimal?>());
        var result = Assert.IsType<CreatedAtActionResult>(response.Result);
        Assert.IsType<ItemResponse>(result.Value);
    }

    [Fact]
    public async Task EditAsync_Returns400_IfNameExists()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        IItemService _service = Substitute.For<IItemService>();
        ILogger<ItemsController> _logger = Substitute.For<ILogger<ItemsController>>();
        ItemsController itemsController = new ItemsController(_service, _logger);

        string name = RandomString.Random(3, 255);
        var model = new EditItemRequest { Id = id, Name = name };

        _service.EditAsync(id, name, null, null).Returns(Task.FromResult(new Optional<ItemDTO>(new BadRequestException("That name already exists"))));

        // Act
        var result = await itemsController.EditAsync(model);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result.Result);
        await _service.Received().EditAsync(Arg.Any<Guid>(), Arg.Any<string?>(), Arg.Any<decimal?>(), Arg.Any<decimal?>());
    }

    [Fact]
    public async Task EditAsync_Returns400_IfGuidEmpty()
    {
        // Arrange
        Guid id = Guid.Empty;
        IItemService _service = Substitute.For<IItemService>();
        ILogger<ItemsController> _logger = Substitute.For<ILogger<ItemsController>>();
        ItemsController itemsController = new ItemsController(_service, _logger);

        string name = RandomString.Random(3, 255);
        var model = new EditItemRequest { Id = id, Name = name };

        // Act
        var result = await itemsController.EditAsync(model);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result.Result);
        await _service.DidNotReceive().EditAsync(Arg.Any<Guid>(), Arg.Any<string?>(), Arg.Any<decimal?>(), Arg.Any<decimal?>());

    }

    [Fact]
    public async Task EditAsync_Returns404_IfGuidNotFound()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        IItemService _service = Substitute.For<IItemService>();
        ILogger<ItemsController> _logger = Substitute.For<ILogger<ItemsController>>();
        ItemsController itemsController = new ItemsController(_service, _logger);

        string name = RandomString.Random(3, 255);
        var model = new EditItemRequest { Id = id, Name = name };

        _service.EditAsync(id, name, null, null).Returns(Task.FromResult(new Optional<ItemDTO>(new NotFoundException("Guid cannot be found"))));

        // Act
        var result = await itemsController.EditAsync(model);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result.Result);
        await _service.Received().EditAsync(Arg.Any<Guid>(), Arg.Any<string?>(), Arg.Any<decimal?>(), Arg.Any<decimal?>());
    }
}