using OpenPay.Interfaces.Data.DataModels.Item;

namespace OpenPay.Interfaces.Data.DataModels.Order;
public struct OrderItemDataDTO
{
    public decimal Amount { get; set; }
    public Guid ItemId { get; set; }
    public ItemDataDTO Item { get; set; }
}