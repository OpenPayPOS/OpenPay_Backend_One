using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace OpenPay.Data.DataModels;
public class ItemDataModel : BaseDataModel
{
    [Required]
    public string Name { get; set; }
    [Precision(16,2)]
    [Required]
    public decimal Price { get; set; }
    [Precision(5,2)]
    [Required]
    public decimal TaxPercentage { get; set; }
    public string ImagePath { get; set; }
}