using OpenPay.Api.Models.Response;
using OpenPay.Interfaces.Services.ServiceModels;

namespace OpenPay.Api.Mappers;

// TODO: write tests
public class ItemMapper : IMapper<ItemResponse, ItemDTO>
{
    public Task<ItemResponse> MapDtoToModelAsync(ItemDTO item)
    {
        return Task.FromResult(new ItemResponse
        {
            Id = item.Id,
            Name = item.Name,
            Price = item.Price,
            TaxPercentage = item.TaxPercentage,
            ImagePath = item.ImagePath
        });
    }
}