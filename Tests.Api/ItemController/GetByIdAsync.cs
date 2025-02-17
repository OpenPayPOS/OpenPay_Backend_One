
using Microsoft.AspNetCore.Mvc;

namespace OpenPay.Tests.Api.ItemController;
public class GetByIdAsync
{
    [Fact]
    public async Task GetById_ReturnsStatus400_IfGuidEmpty()
    {
        // Arrange
        ItemsController itemsController = new ItemsController();

        // Act
        ActionResult<ItemResponse> response = await itemsController.GetByIdAsync(Guid.Empty);

        // Assert
        Assert.IsType<BadRequestObjectResult>(response.Result);
    }

    [Fact]
    public async Task GetById_ReturnsStatus404_IfNotFound()
    {
        // Arrange
        ItemsController itemsController = new ItemsController();

        // Act
        ActionResult<ItemResponse> response = await itemsController.GetByIdAsync(Guid.NewGuid());

        // Assert
        Assert.IsType<NotFoundObjectResult>(response.Result);
    }

    [Fact]
    public async Task GetById_Returns200AndItem_IfFound()
    {
        // Arrange
        ItemsController itemsController = new ItemsController();
        Guid id = Guid.NewGuid();
        // TODO: Add data return in service mock

        // Act
        ActionResult<ItemResponse> response = await itemsController.GetByIdAsync(id);

        // Assert
        Assert.IsType<OkObjectResult>(response.Result);
        Assert.IsType<ItemResponse>(response.Value);

        Assert.Equal(response.Value.Id, id);
    }
}