using OpenPay.Api.Models.Response;
using OpenPay.Interfaces.Services.ServiceModels;

namespace OpenPay.Api.Mappers;
public static class ItemMapper
{
    public static Task<ItemResponse> MapDtoToModelAsync(ItemDTO item)
    {
        return Task.FromResult(new ItemResponse
        {
            Id = item.Id,
            Name = item.Name,
            Price = item.Price,
            TaxPercentage = item.TaxPercentage,
        });
    }
}