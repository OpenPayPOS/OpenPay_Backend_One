namespace OpenPay.Api.Models.Response;

public class ItemResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public decimal TaxPercentage { get; set; }
}
