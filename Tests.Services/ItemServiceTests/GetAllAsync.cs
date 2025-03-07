using Microsoft.Extensions.Logging;
using NSubstitute;
using OpenPay.Interfaces.Data.DataModels.Item;
using OpenPay.Interfaces.Data.Models;
using OpenPay.Interfaces.Data.Repositories;
using OpenPay.Interfaces.Services.ServiceModels;
using OpenPay.Services;

namespace OpenPay.Tests.Services.ItemServiceTests;
public class GetAllAsync
{
    [Fact]
    public async Task GetAllAsync_ReturnsEmptyEnumerable_IfEmpty()
    {
        // Arrange
        IItemRepository _repository = Substitute.For<IItemRepository>();
        IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
        ILogger<ItemService> _logger = Substitute.For<ILogger<ItemService>>();
        ItemService _service = new ItemService(_repository, _logger, _unitOfWork);

        _repository.GetAllAsync().Returns(Array.Empty<ItemDataDTO>().ToAsyncEnumerable());

        List<ItemDTO> items = [];
        
        // Act
        await foreach (var item in _service.GetAllAsync())
        {
            items.Add(item);
        }

        // Assert
        _repository.Received().GetAllAsync();
        Assert.Empty(items);

    }
    [Fact]
    public async Task GetAllAsync_ReturnsAll_IfAny()
    {
        // Arrange
        IItemRepository _repository = Substitute.For<IItemRepository>();
        IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
        ILogger<ItemService> _logger = Substitute.For<ILogger<ItemService>>();
        ItemService _service = new ItemService(_repository, _logger, _unitOfWork);

        _repository
            .GetAllAsync()
            .Returns(new List<ItemDataDTO>
            {
                new ItemDataDTO
                {
                    Id = Guid.NewGuid(),
                    Name = "test",
                    Price = 10,
                    TaxPercentage = 10
                }
            }.ToAsyncEnumerable());

        List<ItemDTO> items = [];

        // Act
        await foreach (var item in _service.GetAllAsync())
        {
            items.Add(item);
        }

        // Assert
        _repository.Received().GetAllAsync();
        Assert.NotEmpty(items);
    }
}