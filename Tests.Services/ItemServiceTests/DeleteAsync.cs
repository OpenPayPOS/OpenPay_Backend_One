using Interfaces.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using OpenPay.Interfaces.Data.DataModels;
using OpenPay.Interfaces.Data.Models;
using OpenPay.Interfaces.Data.Repositories;
using OpenPay.Services;

namespace OpenPay.Tests.Services.ItemServiceTests;
public class DeleteAsync
{
    public async Task DeleteAsync_ReturnsSuccess_IfDeleted()
    {
        // Arrange
        IItemRepository _repository = Substitute.For<IItemRepository>();
        IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
        ILogger<ItemService> _logger = Substitute.For<ILogger<ItemService>>();
        ItemService _service = new ItemService(_repository, _logger, _unitOfWork);

        Guid id = Guid.NewGuid();

        _repository.IdExistsAsync(Arg.Any<Guid>()).Returns(true);

        // Act
        var response = await _service.DeleteAsync(Guid.NewGuid());

        // Assert
        await _repository.Received().IdExistsAsync(Arg.Any<Guid>());
        await _repository.Received().DeleteAsync(Arg.Any<Guid>(), Arg.Any<EditItemDataDTO>());
        await _unitOfWork.Received().SaveChangesAsync();


        response.Handle(_ =>
        {
            return new OkResult();
        }, _ =>
        {
            Assert.Fail();
            return new OkResult();
        });
    }

    public async Task DeleteAsync_ReturnsNotFoundException_IfIdDoesntExist()
    {
        // Arrange
        IItemRepository _repository = Substitute.For<IItemRepository>();
        IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
        ILogger<ItemService> _logger = Substitute.For<ILogger<ItemService>>();
        ItemService _service = new ItemService(_repository, _logger, _unitOfWork);

        Guid id = Guid.NewGuid();

        _repository.IdExistsAsync(Arg.Any<Guid>()).Returns(false);

        // Act
        var response = await _service.DeleteAsync(Guid.NewGuid());

        // Assert
        await _repository.Received().IdExistsAsync(Arg.Any<Guid>());
        await _repository.DidNotReceive().DeleteAsync(Arg.Any<Guid>(), Arg.Any<EditItemDataDTO>());
        await _unitOfWork.DidNotReceive().SaveChangesAsync();


        response.Handle(_ =>
        {
            Assert.Fail();
            return new OkResult();
        }, ex =>
        {
            Assert.IsType<NotFoundException>(ex);
            return new OkResult();
        });
    }
}