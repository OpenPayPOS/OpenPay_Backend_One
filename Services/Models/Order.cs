using OpenPay.Interfaces.Data.DataModels.Order;
using OpenPay.Interfaces.Services.ServiceModels;

namespace OpenPay.Services.Models;
public class Order : BaseModel<Order, OrderDTO, OrderDataDTO>
{
    public Guid Id { get; private set; }
    public List<OrderItem> OrderItems { get; private set; }
    public DateTime CreatedTime { get; private set; }

    private Order(Guid id, List<OrderItem> orderItems, DateTime createdTime)
    {
        Id = id;
        OrderItems = orderItems;
        CreatedTime = createdTime;
    }

    public static Order Create(List<OrderItem> orderItems, DateTime createdTime)
    {
        return new Order(Guid.NewGuid(), orderItems, createdTime);
    }

    public static Order FromDataDTO(OrderDataDTO dataDTO)
    {
        return new Order(dataDTO.Id, dataDTO.OrderItems.Select(OrderItem.FromDataDTO).ToList(), dataDTO.CreatedTime);
    }

    public override OrderDataDTO ToDataDTO()
    {
        return new OrderDataDTO
        {
            Id = Id,
            OrderItems = OrderItems.Select(orderItem => orderItem.ToDataDTO()).ToList(),
            CreatedTime = CreatedTime,
        };
    }

    public async override Task<OrderDTO> ToDTOAsync()
    {
        List<OrderItemDTO> orderItemDTOs = new List<OrderItemDTO>();
        
        foreach (var orderItem in OrderItems)
        {
            orderItemDTOs.Add(await orderItem.ToDTOAsync());
        }

        return new OrderDTO
        {
            Id = Id,
            OrderItems = orderItemDTOs,
            CreatedTime = CreatedTime
        };
    }

    internal CreateOrderDataDTO ToCreateDataDTO()
    {
        return new CreateOrderDataDTO
        {
            Id = Id,
            OrderItems = OrderItems.Select(orderItem => orderItem.ToCreateDataDTO()).ToList(),
            CreatedTime = CreatedTime,
        };
    }
}