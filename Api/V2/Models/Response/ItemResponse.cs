namespace OpenPay.Api.V2.Models.Response;
public class ActiveItemResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string ImagePath { get; set; }
    public decimal ActivePrice { get; set; }
    public decimal ActiveTaxPercentage { get; set; }
}