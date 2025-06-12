namespace OpenPay.Interfaces.Services.ServiceModels;
/// <summary>
/// Internal accounts
/// </summary>
public struct UserAccountDTO
{
    public Guid Id { get; set; }
    public decimal? Balance { get; set; }
    public PriceTypeDTO AllowedPriceType { get; set; }
}