using Interfaces.Common.Models;
using OpenPay.Interfaces.Data.DataModels.Order;
using OpenPay.Interfaces.Data.Repositories.Common;

namespace OpenPay.Interfaces.Data.Repositories;

public interface IOrderRepository : IBaseRepository<OrderDataDTO>
{
    Task<Optional<OrderDataDTO>> CreateAsync(CreateOrderDataDTO orderDataDTO);
}
