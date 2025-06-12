using Interfaces.Common.Models;
using OpenPay.Interfaces.Data.DataModels.Item;
using OpenPay.Interfaces.Data.Repositories.Common;

namespace OpenPay.Interfaces.Data.Repositories;
public interface IItemRepository : IBaseRepository<ItemDataDTO>
{
    Task<Optional<bool>> NameExistsAsync(string name);
    
    Task<Optional<ItemDataDTO>> CreateAsync(ItemDataDTO itemDataDTO);
    Task<Optional<ItemDataDTO>> EditAsync(Guid id, EditItemDataDTO editItemDataDTO);
}