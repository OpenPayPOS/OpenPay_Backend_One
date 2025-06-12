namespace OpenPay.Interfaces.Services.ServiceModels;
public struct PriceDTO
{
    public Guid Id { get; set; }
    public decimal ActiveAmount { get; set; }
    public PriceInstanceDTO ActivePriceInstance { get; set; }
    public PriceTypeDTO PriceType { get; set; }
    public List<PriceInstanceDTO> Prices { get; set; }   
}

public struct PriceInstanceDTO
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreatedTime { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
}