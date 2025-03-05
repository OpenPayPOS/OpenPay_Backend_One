using OpenPay.Api.Models.Response;
using OpenPay.Interfaces.Services.ServiceModels;

namespace OpenPay.Api.Mappers;
public static class OrderMapper
{
    public static async Task<OrderResponse> MapDtoToModelAsync(OrderDTO orderDTO)
    {
        List<OrderItemResponse> orderItems = new List<OrderItemResponse>();
        foreach (var orderItemDTO in orderDTO.Items)
        {
            orderItems.Add(new OrderItemResponse
            {
                Amount = orderItemDTO.Amount,
                Item = await ItemMapper.MapDtoToModelAsync(orderItemDTO.Item)
            });
        };

        return new OrderResponse
        {
            OrderItems = orderItems
        };
    }
}