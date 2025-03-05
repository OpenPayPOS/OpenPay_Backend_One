using OpenPay.Api.Models.Annotations;
using System.ComponentModel.DataAnnotations;

namespace OpenPay.Api.Models.Request;
public class CreateOrderRequest
{
    [Required(ErrorMessage = "Items are required.")]
    [MinLength(1, ErrorMessage = "At least an item is required.")]
    public List<CreateOrderItem>? OrderItems { get; set; }
    [DataType(DataType.Date)]
    public DateTime? CreatedTime { get; set; }
}

public class CreateOrderItem
{
    [Required(ErrorMessage = "Id is required.")]
    public Guid? ItemId { get; set; }

    [Required(ErrorMessage = "Amount is required.")]
    [Range(0, double.MaxValue, ErrorMessage = "Amount must be positive.")]
    [ValidPrice(ErrorMessage = "Amount has to have at most 2 decimals.")]
    public decimal? Amount { get; set; }
}