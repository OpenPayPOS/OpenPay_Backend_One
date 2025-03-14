using OpenPay.Data.DataModels;
using OpenPay.Interfaces.Data.DataModels.Order;

namespace OpenPay.Data.Mappers;

public class OrderMapper : IMapper<OrderDataModel, OrderDataDTO>
{
    public OrderDataDTO MapToDataDTO(OrderDataModel model)
    {
        List<OrderItemDataDTO> orderItems = new List<OrderItemDataDTO>();
        foreach (var orderItem in model.OrderItems)
        {
            orderItems.Add(new OrderItemDataDTO
            {
                Amount = orderItem.Amount,
                ItemId = orderItem.ItemId,
            });
        }

        return new OrderDataDTO
        {
            Id = model.Id,
            CreatedTime = DateTime.MinValue,
            OrderItems = orderItems
        };
    }
}
