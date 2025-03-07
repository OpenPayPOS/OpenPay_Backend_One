namespace OpenPay.Interfaces.Data.DataModels.Order;
public struct CreateOrderDataDTO
{
    public Guid Id { get; set; }
    public List<CreateOrderItemDataDTO> OrderItems { get; set; }
    public DateTime CreatedTime { get; set; }
}

public struct CreateOrderItemDataDTO
{
    public decimal Amount { get; set; }
    public Guid ItemId { get; set; }
}