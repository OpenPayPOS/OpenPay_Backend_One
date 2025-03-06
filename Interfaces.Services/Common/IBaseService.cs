
using Interfaces.Common.Models;

namespace OpenPay.Interfaces.Services.Common;
public interface IBaseService<T> where T : struct
{
    Task<Optional<T>> GetByIdAsync(Guid id);
    Task<Optional> DeleteAsync(Guid id);
}