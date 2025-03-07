using OpenPay.Interfaces.Data.DataModels.Order;
using OpenPay.Interfaces.Services.ServiceModels;

namespace OpenPay.Services.Models;
public class OrderItem : BaseModel<OrderItem, OrderItemDTO, OrderItemDataDTO>
{
    public decimal Amount { get; set; }
    public Item? Item { get; set; }
    public Guid ItemId { get; set; }

    private OrderItem(decimal amount, Guid itemId, Item? item = null)
    {
        Amount = amount;
        ItemId = itemId;
        Item = item;
    }

    internal static List<OrderItem> CreateFromDTOs(List<CreateOrderItemDTO> orderItemDTOs)
    {
        return orderItemDTOs.Select(orderItem => new OrderItem(orderItem.Amount, orderItem.ItemId)).ToList();
    }

    internal static OrderItem FromDataDTO(OrderItemDataDTO orderItem)
    {
        return new OrderItem(orderItem.Amount, orderItem.ItemId, Item.FromDataDTO(orderItem.Item));
    }

    public override OrderItemDataDTO ToDataDTO()
    {
        return new OrderItemDataDTO
        {
            Amount = Amount,
            ItemId = ItemId,
            Item = Item.ToDataDTO()
        };
    }

    public override async Task<OrderItemDTO> ToDTOAsync()
    {
        return new OrderItemDTO
        {
            Amount = Amount,
            Item = await Item.ToDTOAsync()
        };
    }

    internal CreateOrderItemDataDTO ToCreateDataDTO()
    {
        return new CreateOrderItemDataDTO
        {
            Amount = Amount,
            ItemId = ItemId
        };
    }
}