using Microsoft.Extensions.Logging;
using OpenPay.Api.Mappers;
using OpenPay.Api.V1.Controllers.Common;
using OpenPay.Api.V1.Models.Response;
using OpenPay.Interfaces.Services;
using OpenPay.Interfaces.Services.ServiceModels;

namespace OpenPay.Api.V1.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class ItemCategoriesController : BaseController<ItemCategoryDTO, ItemCategoryResponse>
{
    private readonly IItemCategoryService _itemCategoryService;
    private readonly ILogger<ItemCategoriesController> _logger;

    public ItemCategoriesController(IItemCategoryService service, ILogger<ItemCategoriesController> logger)
        : base(service, logger, new ItemCategoryMapper())
    {
        _itemCategoryService = service;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType<ItemCategoryResponse>(200)]
    public async IAsyncEnumerable<ItemCategoryResponse> GetAllAsync()
    {
        await foreach (var itemCategory in await _itemCategoryService.GetAllAsync())
        {
            yield return await _mapper.MapDtoToModelAsync(itemCategory);
        }
    }
}