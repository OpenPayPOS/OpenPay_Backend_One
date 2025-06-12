namespace OpenPay.Interfaces.Services.ServiceModels;
public struct ItemDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public decimal TaxPercentage { get; set; }
    public TaxTypeDTO TaxType { get; set; }
    public string ImagePath { get; set; }
    public PriceDTO ActivePrice { get; set; }
    public Dictionary<PriceTypeDTO, PriceDTO> Prices { get; set; }
}