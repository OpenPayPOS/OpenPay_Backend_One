using Interfaces.Common.Models;
using OpenPay.Interfaces.Services.ServiceModels;

namespace OpenPay.Interfaces.Services;

public interface IItemService
{
    IAsyncEnumerable<ItemDTO> GetAllAsync();
    Task<Optional<ItemDTO>> GetByIdAsync(Guid id);
    Task<Optional<ItemDTO>> CreateAsync(string name, decimal price, decimal taxPercentage);
    Task<Optional<ItemDTO>> EditAsync(Guid id, string? name, decimal? price, decimal? taxPercentage);
    Task<Optional> DeleteAsync(Guid id);
}
