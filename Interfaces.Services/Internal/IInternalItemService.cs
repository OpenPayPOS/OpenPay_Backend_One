using Interfaces.Common.Models;

namespace OpenPay.Interfaces.Services.Internal;
public interface IInternalItemService
{
    public Task<Optional<bool>> IdExistsAsync(Guid id);
}