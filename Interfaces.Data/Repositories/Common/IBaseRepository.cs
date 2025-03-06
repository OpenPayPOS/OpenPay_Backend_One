
using Interfaces.Common.Models;

namespace OpenPay.Interfaces.Data.Repositories.Common;
public interface IBaseRepository<T>
{
    Task<Optional<T>> GetByIdAsync(Guid id);
    Task<Optional<bool>> IdExistsAsync(Guid id);
    Task<Optional> DeleteAsync(Guid id);
}