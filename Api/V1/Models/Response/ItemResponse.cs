namespace OpenPay.Api.V1.Models.Response;

public class ItemResponse
{
    public Guid Id { get; set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public string Name { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public decimal Price { get; set; }
    public decimal TaxPercentage { get; set; }
    public string ImagePath { get; set; }
}
