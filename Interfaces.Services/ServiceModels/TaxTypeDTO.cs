namespace OpenPay.Interfaces.Services.ServiceModels;
public struct TaxTypeDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public TaxTypeInstanceDTO? ActiveInstance { get; set; }
    public List<TaxTypeInstanceDTO>? Instances { get; set; }
}

public struct TaxTypeInstanceDTO
{
    public Guid Id { set; get; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public DateTime CreatedTime { get; set; }
    public decimal Percentage { get; set; }
}