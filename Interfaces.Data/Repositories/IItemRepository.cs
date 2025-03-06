using Interfaces.Common.Models;
using OpenPay.Interfaces.Data.DataModels;
using OpenPay.Interfaces.Data.Repositories.Common;

namespace OpenPay.Interfaces.Data.Repositories;
public interface IItemRepository : IBaseRepository<ItemDataDTO>
{
    IAsyncEnumerable<ItemDataDTO> GetAllAsync();
    Task<Optional<bool>> NameExistsAsync(string name);
    
    Task<Optional<ItemDataDTO>> CreateAsync(ItemDataDTO itemDataDTO);
    Task<Optional<ItemDataDTO>> EditAsync(Guid id, EditItemDataDTO editItemDataDTO);
}