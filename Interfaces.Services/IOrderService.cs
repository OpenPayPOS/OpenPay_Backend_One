
using Interfaces.Common.Models;
using OpenPay.Interfaces.Services.ServiceModels;

namespace OpenPay.Interfaces.Services;

public interface IOrderService
{
    Task<Optional<OrderDTO>> CreateAsync(List<CreateOrderItemDTO> orderItems, DateTime? createdTime = null);
    Task<Optional<OrderDTO>> GetByIdAsync(Guid id);
    Task<Optional<bool>> IdExistsAsync(Guid itemId);
}
