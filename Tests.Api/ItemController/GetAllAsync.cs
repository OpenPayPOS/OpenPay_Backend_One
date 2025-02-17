namespace OpenPay.Tests.Api.ItemController;
public class GetAllAsync
{
    [Fact]
    public async Task GetAllAsync_ReturnsEmptyEnumerable_IfEmpty()
    {
        // Arrange
        ItemsController itemsController = new ItemsController();
        List<ItemResponse> items = new List<ItemResponse>();

        // Act
        await foreach (var item in itemsController.GetAllAsync())
        {
            items.Add(item);
        }

        // Assert
        // TODO: Add service ran once
        Assert.Empty(items);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAll_IfAny()
    {
        // Arrange
        ItemsController itemsController = new ItemsController();
        List<ItemResponse> items = new List<ItemResponse>();
        // TODO: Add data return in service mock

        // Act
        await foreach (var item in itemsController.GetAllAsync())
        {
            items.Add(item);
        }

        // Assert
        // TODO: Add service ran once
        Assert.NotEmpty(items);
    }
}