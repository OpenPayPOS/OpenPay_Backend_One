using Interfaces.Common.Models;
using OpenPay.Interfaces.Services.Common;
using OpenPay.Interfaces.Services.ServiceModels;

namespace OpenPay.Interfaces.Services;

public interface IItemService : IBaseService<ItemDTO>
{
    IAsyncEnumerable<ItemDTO> GetAllAsync();
    Task<Optional<ItemDTO>> CreateAsync(string name, decimal price, decimal taxPercentage, string fileName);
    Task<Optional<ItemDTO>> EditAsync(Guid id, string? name, decimal? price, decimal? taxPercentage);
}
