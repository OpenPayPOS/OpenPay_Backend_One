using Microsoft.AspNetCore.Mvc;

namespace OpenPay.Tests.Api.ItemController;
public class DeleteAsync
{
    [Fact]
    public async Task DeleteAsync_Returns400_IfGuidEmpty()
    {
        // Arrange
        Guid id = Guid.Empty;
        ItemsController itemsController = new ItemsController();

        // Act
        var response = await itemsController.DeleteAsync(id);

        // Assert
        Assert.IsType<BadRequestObjectResult>(response);
    }

    [Fact]
    public async Task DeleteAsync_Returns404_IfGuidNotFound()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        ItemsController itemsController = new ItemsController();

        // Act
        var response = await itemsController.DeleteAsync(id);

        // Assert
        Assert.IsType<NotFoundObjectResult>(response);
    }

    [Fact]
    public async Task DeleteAsync_Returns204_IfDeleted()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        ItemsController itemsController = new ItemsController();
        // TODO: add data to mock

        // Act
        var response = await itemsController.DeleteAsync(id);

        // Assert
        Assert.IsType<NoContentResult>(response);
        // TODO: assert mock ran once
    }
}