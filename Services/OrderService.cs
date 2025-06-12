using Interfaces.Common.Exceptions;
using Interfaces.Common.Models;
using Microsoft.Extensions.Logging;
using OpenPay.Interfaces.Data.DataModels.Order;
using OpenPay.Interfaces.Data.Models;
using OpenPay.Interfaces.Data.Repositories;
using OpenPay.Interfaces.Services;
using OpenPay.Interfaces.Services.Internal;
using OpenPay.Interfaces.Services.ServiceModels;
using OpenPay.Services.Common;
using OpenPay.Services.Models;

namespace OpenPay.Services;
public class OrderService : BaseService<Order, OrderDTO, OrderDataDTO>, IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IInternalItemService _itemService;
    public OrderService(IOrderRepository repository,
        ILogger<BaseService<Order, OrderDTO, OrderDataDTO>> logger,
        IUnitOfWork unitOfWork, IInternalItemService itemService)
        : base(repository, logger, unitOfWork)
    {
        _orderRepository = repository;
        _itemService = itemService;
    }

    public async Task<Optional<OrderDTO>> CreateAsync(List<CreateOrderItemDTO> orderItemDTOs, DateTime? createdTime = null)
    {
        foreach (var orderItem in orderItemDTOs)
        {
            var existsOptional = await _itemService.IdExistsAsync(orderItem.ItemId);

            if (existsOptional.IsInvalid) return (Exception)existsOptional;

            if (existsOptional.Handle(exists => !exists, _ => throw new Exception()))
            {
                return new BadRequestException("Item with that id does not exist.");
            }
        }
        // From here we are sure all ID's exist

        if (createdTime == null) createdTime = DateTime.UtcNow;

        List<OrderItem> orderItems = OrderItem.CreateFromDTOs(orderItemDTOs);
        Order order = Order.Create(orderItems, (DateTime)createdTime);

        var orderOptional = await _orderRepository.CreateAsync(order.ToCreateDataDTO());

        return await orderOptional.HandleAsync(async data =>
        {
            await _unitOfWork.SaveChangesAsync();
            Order order = Order.FromDataDTO(data);

            return new Optional<OrderDTO>(await order.ToDTOAsync());
        }, ex => { return ex; });
    }

    public override Order FromDataDTO(OrderDataDTO dataDTO)
    {
        return Order.FromDataDTO(dataDTO);
    }
}