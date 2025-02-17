using Microsoft.AspNetCore.Mvc;
using OpenPay.Api.Models.Request;
using OpenPay.Tests.Api.Helpers;

namespace OpenPay.Tests.Api.ItemController;
public class CreateAsync
{
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(256)]
    public async Task CreateAsync_WrongNameLength_ThrowsError(int nameLength)
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
    public async Task CreateAsync_WrongPrice_ThrowsError(decimal price)
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
    public async Task CreateAsync_WrongPercentage_ThrowsError(decimal percentage)
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
        var model = new CreateItemRequest { Name = RandomString.Random(3, 255), Price = 10, TaxPercentage = 1 };
        ItemsController itemsController = new ItemsController();

        // Act
        var result = await itemsController.CreateAsync(model);

        // Assert
        // TODO: assert mock ran once
        Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.IsType<ItemResponse>(result.Value);
    }

    [Fact]
    public async Task CreateAsync_Returns400_IfNameExists()
    {
        // Arrange
        string name = RandomString.Random(3, 255);
        var model = new CreateItemRequest { Name = name, Price = 10, TaxPercentage = 1 };
        ItemsController itemsController = new ItemsController();
        // TODO: Add data in service mock

        // Act
        var result = await itemsController.CreateAsync(model);

        // Assert
        // TODO: assert mock ran once
        Assert.IsType<BadRequestObjectResult>(result.Result);
    }
}