using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenPay.Data.DataModels;

public class OrderDataModel : BaseDataModel
{
    [InverseProperty("Order")]
    public ICollection<OrderItemDataModel> OrderItems { get; set; } = new List<OrderItemDataModel>();

    public DateTime OrderPlaced { get; set; }
}
