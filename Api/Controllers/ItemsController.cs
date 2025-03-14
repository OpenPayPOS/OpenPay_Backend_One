namespace OpenPay.Api.Controllers;

using OpenPay.Api.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenPay.Api.Models.Request;
using OpenPay.Api.Models.Response;
using OpenPay.Interfaces.Services;
using OpenPay.Interfaces.Services.ServiceModels;
using OpenPay.Api.Mappers;
using OpenPay.Api.Controllers.Common;
using Microsoft.AspNetCore.Authorization;

[Route("api/v1/[controller]")]
[ApiController]
public class ItemsController : BaseController<ItemDTO, ItemResponse>
{
    private readonly IItemService _itemService;
    private readonly ILogger<ItemsController> _logger;

    public ItemsController(IItemService itemService, ILogger<ItemsController> logger)
        : base (itemService, logger, new ItemMapper())
    {
        _itemService = itemService;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async IAsyncEnumerable<ItemResponse> GetAllAsync()
    {
        await foreach (var item in _itemService.GetAllAsync())
        {
            yield return await _mapper.MapDtoToModelAsync(item);
        } 
    }

    [HttpPost]
    [ProducesResponseType<ItemResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ItemResponse>> CreateAsync([FromForm] CreateItemRequest item)
    {
        var file = item.Image;
        var slnPath = Directory.GetParent(Directory.GetCurrentDirectory()).ToString();
        var uploadFolder = Path.Combine(slnPath, "ImageServer", "wwwroot", "uploads");

        if (!Directory.Exists(uploadFolder))
        {
            Directory.CreateDirectory(uploadFolder);
        }

        string guid = Guid.NewGuid().ToString();
        string fileType = file.FileName.Split('.').Reverse().ToList()[0];
        var filePath = Path.Combine(uploadFolder, $"{guid}.{fileType}");
        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream).ConfigureAwait(false);
        }

        var itemOptional = await _itemService.CreateAsync(item.Name, item.Price, item.TaxPercentage, $"{guid}.{fileType}");

        return await itemOptional.ProduceResultAsync(async itemDTO =>
        {
            return CreatedAtAction(nameof(GetByIdAsync), new { id = itemDTO.Id }, await _mapper.MapDtoToModelAsync(itemDTO));
        }, _exceptionHandler.HandleException);
    }

    [HttpPatch]
    [ProducesResponseType<ItemResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ItemResponse>> EditAsync([FromBody] EditItemRequest item)
    {
        if (item.Id == Guid.Empty) return BadRequest("Guid cannot be empty");
        var itemOptional = await _itemService.EditAsync(item.Id, item.Name, item.Price, item.TaxPercentage);

        return await itemOptional.ProduceResultAsync(async itemDTO =>
        {
            return CreatedAtAction(nameof(GetByIdAsync), new { id = itemDTO.Id }, await _mapper.MapDtoToModelAsync(itemDTO));
        }, _exceptionHandler.HandleException);
    }
}
