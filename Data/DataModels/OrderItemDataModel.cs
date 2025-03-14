using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace OpenPay.Data.DataModels;

public class OrderItemDataModel : BaseDataModel
{

    [Precision(16, 2)]
    [Required]
    public decimal Amount { get; set; }
    public Guid ItemId { get; set; }
    [Required]
    [ForeignKey("ItemId")]
    public ItemDataModel Item { get; set; }
    [Required]
    [ForeignKey(nameof(OrderDataModel))]
    public Guid OrderId { get; set; } // Required foreign key property
    public OrderDataModel Order { get; set; } = null!;
}
