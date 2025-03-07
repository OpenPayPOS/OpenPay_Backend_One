using Interfaces.Common.Exceptions;
using Interfaces.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using OpenPay.Interfaces.Data.DataModels.Item;
using OpenPay.Interfaces.Data.Models;
using OpenPay.Interfaces.Data.Repositories;
using OpenPay.Services;

namespace OpenPay.Tests.Services.ItemServiceTests;
public class EditAsync
{
    [Fact]
    public async Task EditAsync_ReturnsItem_IfEdited()
    {
        // Arrange
        IItemRepository _repository = Substitute.For<IItemRepository>();
        IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
        ILogger<ItemService> _logger = Substitute.For<ILogger<ItemService>>();
        ItemService _service = new ItemService(_repository, _logger, _unitOfWork);

        Guid id = Guid.NewGuid();

        _repository.NameExistsAsync("test").Returns(false);
        _repository.IdExistsAsync(Arg.Any<Guid>()).Returns(true);
        _repository.GetByIdAsync(Arg.Any<Guid>()).Returns(new Optional<ItemDataDTO>());
        _repository.EditAsync(Arg.Any<Guid>(), Arg.Any<EditItemDataDTO>()).Returns(new ItemDataDTO
        {
            Id = id,
            Name = "asdf",
            Price = 11,
            TaxPercentage = 11
        });

        // Act
        var response = await _service.EditAsync(Guid.NewGuid(), "test", 10, null);

        // Assert
        await _repository.Received().IdExistsAsync(Arg.Any<Guid>());
        await _repository.Received().NameExistsAsync(Arg.Any<string>());
        await _repository.Received().EditAsync(Arg.Any<Guid>(), Arg.Any<EditItemDataDTO>());
        await _unitOfWork.Received().SaveChangesAsync();

        response.Handle(value =>
        {
            Assert.Equal(id, value.Id);
        }, _ =>
        {
            Assert.Fail();
        });
    }

    [Fact]
    public async Task EditAsync_ReturnsBadRequestException_IfNameExists()
    {
        // Arrange
        IItemRepository _repository = Substitute.For<IItemRepository>();
        IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
        ILogger<ItemService> _logger = Substitute.For<ILogger<ItemService>>();
        ItemService _service = new ItemService(_repository, _logger, _unitOfWork);

        Guid id = Guid.NewGuid();

        _repository.NameExistsAsync("test").Returns(true);
        _repository.IdExistsAsync(Arg.Any<Guid>()).Returns(true);

        // Act
        var response = await _service.EditAsync(Guid.NewGuid(), "test", 10, 10);

        // Assert
        await _repository.Received().IdExistsAsync(Arg.Any<Guid>());
        await _repository.Received().NameExistsAsync(Arg.Any<string>());
        await _repository.DidNotReceive().EditAsync(Arg.Any<Guid>(), Arg.Any<EditItemDataDTO>());
        await _unitOfWork.DidNotReceive().SaveChangesAsync();


        response.Handle(_ =>
        {
            Assert.Fail();
        }, ex =>
        {
            Assert.IsType<BadRequestException>(ex);
        });
    }

    [Fact]
    public async Task EditAsync_ReturnsNotFoundException_IfIdDoesntExist()
    {
        // Arrange
        IItemRepository _repository = Substitute.For<IItemRepository>();
        IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
        ILogger<ItemService> _logger = Substitute.For<ILogger<ItemService>>();
        ItemService _service = new ItemService(_repository, _logger, _unitOfWork);

        Guid id = Guid.NewGuid();

        _repository.IdExistsAsync(Arg.Any<Guid>()).Returns(false);

        // Act
        var response = await _service.EditAsync(Guid.NewGuid(), "test", 10, 10);

        // Assert
        await _repository.Received().IdExistsAsync(Arg.Any<Guid>());
        await _repository.DidNotReceive().NameExistsAsync(Arg.Any<string>());
        await _repository.DidNotReceive().EditAsync(Arg.Any<Guid>(), Arg.Any<EditItemDataDTO>());
        await _unitOfWork.DidNotReceive().SaveChangesAsync();


        response.Handle(_ =>
        {
            Assert.Fail();
        }, ex =>
        {
            Assert.IsType<NotFoundException>(ex);
        });
    }
}