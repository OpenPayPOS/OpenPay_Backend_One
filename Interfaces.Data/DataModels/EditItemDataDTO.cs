namespace OpenPay.Interfaces.Data.DataModels;
public struct EditItemDataDTO
{
    public string? Name { get; set; }
    public decimal? Price { get; set; }
    public decimal? TaxPercentage { get; set; }
}