namespace OpenPay.Interfaces.Data.DataModels.Item;
public struct ItemDataDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public decimal TaxPercentage { get; set; }
    public string ImagePath { get; set; }
}