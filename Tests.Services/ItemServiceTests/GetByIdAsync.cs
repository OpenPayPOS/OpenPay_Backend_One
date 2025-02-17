using Interfaces.Common.Exceptions;
using Interfaces.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using OpenPay.Interfaces.Data.DataModels;
using OpenPay.Interfaces.Data.Models;
using OpenPay.Interfaces.Data.Repositories;
using OpenPay.Interfaces.Services.ServiceModels;
using OpenPay.Services;

namespace OpenPay.Tests.Services.ItemServiceTests;
public class GetByIdAsync
{
    [Fact]
    public async Task GetById_ReturnsNotFoundException_IfNotFound()
    {
        // Arrange
        IItemRepository _repository = Substitute.For<IItemRepository>();
        IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
        ILogger<ItemService> _logger = Substitute.For<ILogger<ItemService>>();
        ItemService _service = new ItemService(_repository, _logger, _unitOfWork);

        Guid id = Guid.NewGuid();

        _repository.GetByIdAsync(id).Returns(new Optional<ItemDataDTO>());
        _repository.IdExistsAsync(id).Returns(false);

        // Act
        var response = await _service.GetByIdAsync(id);

        // Assert
        await _repository.Received().IdExistsAsync(Arg.Any<Guid>());
        await _repository.DidNotReceive().GetByIdAsync(Arg.Any<Guid>());
        response.Handle(_ =>
        {
            Assert.Fail();
        }, ex =>
        {
            Assert.IsType<NotFoundException>(ex);
        });
    }

    [Fact]
    public async Task GetById_ReturnsItem_IfFound()
    {
        // Arrange
        IItemRepository _repository = Substitute.For<IItemRepository>();
        IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
        ILogger<ItemService> _logger = Substitute.For<ILogger<ItemService>>();
        ItemService _service = new ItemService(_repository, _logger, _unitOfWork);

        Guid id = Guid.NewGuid();

        _repository.GetByIdAsync(id).Returns(new ItemDataDTO
        {
            Id = id,
            Name = "test",
        });
        _repository.IdExistsAsync(id).Returns(true);

        // Act
        var response = await _service.GetByIdAsync(id);

        // Assert
        await _repository.Received().IdExistsAsync(Arg.Any<Guid>());
        await _repository.Received().GetByIdAsync(Arg.Any<Guid>());
        response.Handle(item =>
        {
            Assert.Equal(item.Id, id);
        }, _ =>
        {
            Assert.Fail();
        });
    }
}