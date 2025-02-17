using Interfaces.Common.Models;
using Microsoft.Extensions.Logging;
using OpenPay.Interfaces.Data.Models;
using OpenPay.Interfaces.Data.Repositories;
using OpenPay.Interfaces.Services;
using OpenPay.Interfaces.Services.ServiceModels;

namespace OpenPay.Services;
public class ItemService : IItemService
{
    private readonly IItemRepository _itemRepository;
    private readonly ILogger<ItemService> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public ItemService(IItemRepository itemRepository, ILogger<ItemService> logger, IUnitOfWork unitOfWork)
    {
        _itemRepository = itemRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public IAsyncEnumerable<ItemDTO> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Optional<ItemDTO>> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
    public Task<Optional<ItemDTO>> CreateAsync(string name, decimal price, decimal taxPercentage)
    {
        throw new NotImplementedException();
    }

    public Task<Optional<ItemDTO>> EditAsync(Guid id, string? name, decimal? price, decimal? taxPercentage)
    {
        throw new NotImplementedException();
    }

    public Task<Optional> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}