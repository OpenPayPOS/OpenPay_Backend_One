using Interfaces.Common.Models;
using OpenPay.Interfaces.Data.DataModels;

namespace OpenPay.Interfaces.Data.Repositories;
public interface IItemRepository
{
    IAsyncEnumerable<ItemDataDTO> GetAllAsync();
    Task<Optional<ItemDataDTO?>> GetByIdAsync(Guid id);
    
    Task<Optional<bool>> IdExistsAsync(Guid id);
    Task<Optional<bool>> NameExistsAsync(string name);
    
    Task<Optional<ItemDataDTO>> CreateAsync(ItemDataDTO itemDataDTO);
    Task<Optional<ItemDataDTO>> EditAsync(Guid id, EditItemDataDTO editItemDataDTO);
    Task DeleteAsync(Guid guid, EditItemDataDTO editItemDataDTO);
}