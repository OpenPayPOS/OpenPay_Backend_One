using OpenPay.Api.Models.Annotations;
using System.ComponentModel.DataAnnotations;

namespace OpenPay.Api.Models.Request;

public class EditItemRequest
{
    [Required(ErrorMessage = "Id is required.")]
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public decimal? Price { get; set; }

    public decimal? TaxPercentage { get; set; }
}
