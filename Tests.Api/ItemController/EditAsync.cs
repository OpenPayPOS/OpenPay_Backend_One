using Microsoft.AspNetCore.Mvc;
using OpenPay.Api.Models.Request;
using OpenPay.Tests.Api.Helpers;

namespace OpenPay.Tests.Api.ItemController;
public class EditAsync
{
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(256)]
    public async Task EditAsync_WrongNameLength_ThrowsError(int nameLength)
    {
        // This is done by model validation
        // Arrange
        Guid id = Guid.NewGuid();
        string name = RandomString.Random(nameLength);
        var model = new EditItemRequest { Id = id, Name = name };
        // TODO: add data in mock

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
    public async Task EditAsync_WrongPrice_ThrowsError(int price)
    {
        // This is done by model validation
        // Arrange
        Guid id = Guid.NewGuid();
        var model = new EditItemRequest { Id = id, Price = price};
        // TODO: add data in mock

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
    public async Task EditAsync_WrongPercentage_ThrowsError(int percentage)
    {
        // This is done by model validation
        // Arrange
        Guid id = Guid.NewGuid();
        var model = new EditItemRequest { Id = id, TaxPercentage = percentage };
        // TODO: add data in mock

        // Act
        var validationResults = ModelValidationHelper.ValidateModel(model);

        // Assert
        Assert.NotEmpty(validationResults);
    }

    [Theory]
    [InlineData(false, null, null)]
    [InlineData(true, null, null)]
    [InlineData(false, 1, null)]
    [InlineData(false, null, 1)]
    [InlineData(true, 1, 1)]
    public async Task EditAsync_Returns201AndItem_IfEdited(bool generateName, decimal? price, decimal? taxPercentage)
    {
        // Arrange
        string? name = generateName ? RandomString.Random(3, 255) : null;
        Guid id = Guid.NewGuid();

        var model = new EditItemRequest { Id = id, Name = RandomString.Random(3, 255), Price = price, TaxPercentage = taxPercentage };
        ItemsController itemsController = new ItemsController();
        // TODO: add data in mock

        // Act
        var result = await itemsController.EditAsync(model);

        // Assert
        // TODO: assert mock ran once
        Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.IsType<ItemResponse>(result.Value);
    }

    [Fact]
    public async Task EditAsync_Returns400_IfNameExists()
    {
        // Arrange
        Guid id = Guid.NewGuid();

        string name = RandomString.Random(3, 255);
        var model = new EditItemRequest { Id = id, Name = name };
        ItemsController itemsController = new ItemsController();
        // TODO: Add data in service mock

        // Act
        var result = await itemsController.EditAsync(model);

        // Assert
        // TODO: assert mock ran once
        Assert.IsType<BadRequestObjectResult>(result.Result);
    }

    [Fact]
    public async Task EditAsync_Returns400_IfGuidEmpty()
    {
        // Arrange
        Guid id = Guid.Empty;

        string name = RandomString.Random(3, 255);
        var model = new EditItemRequest { Id = id, Name = name };
        ItemsController itemsController = new ItemsController();

        // Act
        var result = await itemsController.EditAsync(model);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result.Result);
    }

    [Fact]
    public async Task EditAsync_Returns404_IfGuidNotFound()
    {
        // Arrange
        Guid id = Guid.NewGuid();

        string name = RandomString.Random(3, 255);
        var model = new EditItemRequest { Id = id, Name = name };
        ItemsController itemsController = new ItemsController();

        // Act
        var result = await itemsController.EditAsync(model);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result.Result);
    }
}