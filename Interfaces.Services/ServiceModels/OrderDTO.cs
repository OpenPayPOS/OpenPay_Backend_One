namespace OpenPay.Interfaces.Services.ServiceModels;
public struct OrderDTO
{
    public Guid Id { get; set; }
    public List<OrderItemDTO> OrderItems { get; set; }
    public DateTime CreatedTime { get; set; }
    public UserAccountDTO? OrderedAccount { get; set; }
    public UserDTO? UserCreated { get; set; }
}

public struct OrderItemDTO
{
    public decimal Amount { get; set; }
    public ItemDTO Item { get; set; }
}

public struct CreateOrderItemDTO
{
    public decimal Amount { get; set; }
    public Guid ItemId { get; set; }
}