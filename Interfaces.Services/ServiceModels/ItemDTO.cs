namespace OpenPay.Interfaces.Services.ServiceModels;
public struct ItemDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public decimal TaxPercentage { get; set; }
}