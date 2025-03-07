namespace OpenPay.Interfaces.Data.DataModels.Order;
public struct OrderDataDTO
{
    public Guid Id { get; set; }
    public List<OrderItemDataDTO> OrderItems { get; set; }
    public DateTime CreatedTime { get; set; }
}