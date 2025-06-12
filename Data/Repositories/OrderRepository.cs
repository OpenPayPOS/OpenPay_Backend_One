using Interfaces.Common.Models;
using Microsoft.EntityFrameworkCore;
using OpenPay.Data.DataModels;
using OpenPay.Data.Mappers;
using OpenPay.Interfaces.Data.DataModels.Order;
using OpenPay.Interfaces.Data.Repositories;

namespace OpenPay.Data.Repositories;

public class OrderRepository : BaseRepository<OrderDataModel, OrderDataDTO>, IOrderRepository
{
    private readonly ILogger<OrderRepository> _logger;
    public OrderRepository(AppDbContext dbContext, ILogger<OrderRepository> logger)
        : base(dbContext.Orders, logger, new OrderMapper())
    {
        _logger = logger;
    }

    public async Task<Optional<OrderDataDTO>> CreateAsync(CreateOrderDataDTO orderDataDTO)
    {
        OrderDataModel orderDataModel = new OrderDataModel { Id = orderDataDTO.Id };
        List<OrderItemDataModel> orderItems = new List<OrderItemDataModel>();
        
        foreach (var orderItem in orderDataDTO.OrderItems)
        {
            orderItems.Add(new OrderItemDataModel
            {
                OrderId = orderDataModel.Id,
                Amount = orderItem.Amount,
                ItemId = orderItem.ItemId,
                Id = Guid.NewGuid(),
            });
        }

        orderDataModel.OrderItems = orderItems;
        await _set.AddAsync(orderDataModel);

        return _mapper.MapToDataDTO(orderDataModel);
    }
}
