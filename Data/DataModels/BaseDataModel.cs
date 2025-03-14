using System.ComponentModel.DataAnnotations;

namespace OpenPay.Data.DataModels;

public class BaseDataModel
{
    [Key]
    public Guid Id { get; set; }

}
