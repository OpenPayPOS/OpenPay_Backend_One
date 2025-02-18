using Microsoft.EntityFrameworkCore;

namespace OpenPay.Data.DataModels;
public class ItemDataModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    [Precision(2)]
    public decimal Price { get; set; }
    [Precision(2)]
    public decimal TaxPercentage { get; set; }
}