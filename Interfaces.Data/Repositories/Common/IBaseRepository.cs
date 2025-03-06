
using Interfaces.Common.Models;
using OpenPay.Interfaces.Data.DataModels;

namespace OpenPay.Interfaces.Data.Repositories.Common;
public interface IBaseRepository<T>
{
    public IAsyncEnumerable<T> GetAllAsync();
    Task<Optional<T>> GetByIdAsync(Guid id);
    Task<Optional<bool>> IdExistsAsync(Guid id);
    Task<Optional> DeleteAsync(Guid id);
}