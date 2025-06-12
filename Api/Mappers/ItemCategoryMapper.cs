using OpenPay.Api.V1.Models.Response;
using OpenPay.Interfaces.Services.ServiceModels;

namespace OpenPay.Api.Mappers;
public class ItemCategoryMapper : IMapper<ItemCategoryResponse, ItemCategoryDTO>
{
    public Task<ItemCategoryResponse> MapDtoToModelAsync(ItemCategoryDTO itemCategory)
    {
        throw new NotImplementedException();
        // TODO: implement
    }
}