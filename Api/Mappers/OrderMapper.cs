using OpenPay.Api.Models.Response;
using OpenPay.Interfaces.Services.ServiceModels;

namespace OpenPay.Api.Mappers;

// TODO: write tests
public class OrderMapper : IMapper<OrderResponse, OrderDTO>
{
    public async Task<OrderResponse> MapDtoToModelAsync(OrderDTO orderDTO)
    {
        ItemMapper itemMapper = new ItemMapper();
        List<OrderItemResponse> orderItems = new List<OrderItemResponse>();
        foreach (var orderItemDTO in orderDTO.OrderItems)
        {
            orderItems.Add(new OrderItemResponse
            {
                Amount = orderItemDTO.Amount,
                Item = await itemMapper.MapDtoToModelAsync(orderItemDTO.Item)
            });
        };

        return new OrderResponse
        {
            OrderItems = orderItems
        };
    }
}