using OpenPay.Api.V1.Models.Annotations;
using System.ComponentModel.DataAnnotations;

namespace OpenPay.Api.V1.Models.Request;

public class EditItemRequest
{
    [Required(ErrorMessage = "Id is required.")]
    public Guid Id { get; set; }

    [MinLength(3, ErrorMessage = "Name must be at least 3 characters long.")]
    [MaxLength(255, ErrorMessage = "Name must be at most 255 characters long.")]
    public string? Name { get; set; }

    [ValidPrice(ErrorMessage = "Price has to have at most 2 decimals.")]
    [Range(0, double.MaxValue, ErrorMessage = "Price must be positive.")]
    public decimal? Price { get; set; }

    [Range(0, 100, ErrorMessage = "Tax percentage must be between 0 and 100.")]
    [ValidPrice(ErrorMessage = "Percentage has to have at most 2 decimals.")]
    public decimal? TaxPercentage { get; set; }
}
