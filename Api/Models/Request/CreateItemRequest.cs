using Microsoft.AspNetCore.Http;
using OpenPay.Api.Models.Annotations;
using System.ComponentModel.DataAnnotations;

namespace OpenPay.Api.Models.Request;


public class CreateItemRequest
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    [Required(ErrorMessage = "Name is required.")]
    [MinLength(3, ErrorMessage = "Name must be at least 3 characters long.")]
    [MaxLength(255, ErrorMessage = "Name must be at most 255 characters long.")]
    public string Name { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    [Required(ErrorMessage = "Price is required.")]
    [ValidPrice(ErrorMessage = "Price has to have at most 2 decimals.")]
    [Range(0, double.MaxValue, ErrorMessage = "Price must be positive.")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Tax percentage is required.")]
    [Range(0d, 100d, ErrorMessage = "Tax percentage must be between 0 and 100.")]
    [ValidPrice(ErrorMessage = "Percentage has to have at most 2 decimals.")]
    public decimal TaxPercentage { get; set; }

    [Required(ErrorMessage = "Image is required")]
    [DataType(DataType.Upload)]
    [AllowedFileTypes([".jpg", ".png"])]
    public IFormFile Image { get; set; }
}
