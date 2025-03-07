using OpenPay.Data.DataModels;
using OpenPay.Interfaces.Data.DataModels.Item;

namespace OpenPay.Data.Mappers;

public class ItemMapper : IMapper<ItemDataModel, ItemDataDTO>
{
    public ItemDataDTO MapToDataDTO(ItemDataModel itemDataModel)
    {
        return new ItemDataDTO
        {
            Id = itemDataModel.Id,
            Name = itemDataModel.Name,
            Price = itemDataModel.Price,
            TaxPercentage = itemDataModel.TaxPercentage,
        };
    }
}
