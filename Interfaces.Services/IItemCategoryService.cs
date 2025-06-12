using OpenPay.Interfaces.Services.Common;
using OpenPay.Interfaces.Services.ServiceModels;

namespace OpenPay.Interfaces.Services;
public interface IItemCategoryService : IBaseService<ItemCategoryDTO>
{
    Task<IAsyncEnumerable<ItemCategoryDTO>> GetAllAsync();
}