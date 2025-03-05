namespace OpenPay.Api.Models.Response;
public class OrderResponse
{
    public List<OrderItemResponse> OrderItems { get; set; }
}

public class OrderItemResponse
{
    public decimal Amount { get; set; }
    public ItemResponse Item { get; set; }
}