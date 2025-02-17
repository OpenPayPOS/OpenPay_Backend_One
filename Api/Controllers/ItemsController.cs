namespace OpenPay.Api.Controllers;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenPay.Api.Models.Request;
using OpenPay.Api.Models.Response;
using OpenPay.Interfaces.Services;
using OpenPay.Interfaces.Services.ServiceModels;

[Route("api/v1/[controller]")]
[ApiController]
public class ItemsController : ControllerBase
{
    private readonly IItemService _itemService;
    private readonly ILogger<ItemsController> _logger;

    public ItemsController(IItemService itemService, ILogger<ItemsController> logger)
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
            yield return await MapDtoToModelAsync(item);
        } 
    }

    [HttpGet("{id}")]
    [ProducesResponseType<ItemResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ItemResponse>> GetByIdAsync(Guid id)
    {
        if (id == Guid.Empty) return BadRequest("Id cannot be empty.");

        var itemOptional = await _itemService.GetByIdAsync(id);

        if (itemOptional.IsInvalid) return HandleException(itemOptional);

        return Ok(await MapDtoToModelAsync(itemOptional));
    }

    [HttpPost]
    [ProducesResponseType<ItemResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ItemResponse>> CreateAsync([FromBody] CreateItemRequest item)
    {
        var itemOptional = await _itemService.CreateAsync(item.Name, item.Price, item.TaxPercentage);

        if (itemOptional.IsInvalid) return HandleException(itemOptional);

        return CreatedAtAction(nameof(GetByIdAsync), new { id = itemOptional.Value.Id }, await MapDtoToModelAsync(itemOptional));
    }

    [HttpPatch]
    [ProducesResponseType<ItemResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ItemResponse>> EditAsync([FromBody] EditItemRequest item)
    {
        if (item.Id == Guid.Empty) return BadRequest("Guid cannot be empty");
        var itemOptional = await _itemService.EditAsync(item.Id, item.Name, item.Price, item.TaxPercentage);

        if (itemOptional.IsInvalid) return HandleException(itemOptional);

        return CreatedAtAction(nameof(GetByIdAsync), new { id = itemOptional.Value.Id }, await MapDtoToModelAsync(itemOptional));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        if (id == Guid.Empty) return BadRequest("Guid cannot be empty");
        var optional = await _itemService.DeleteAsync(id);

        if (optional.IsInvalid) return HandleException(optional);

        return NoContent();
    }

    private static Task<ItemResponse> MapDtoToModelAsync(ItemDTO item)
    {
        return Task.FromResult(new ItemResponse
        {
            Id = item.Id,
            Name = item.Name,
            Price = item.Price,
            TaxPercentage = item.TaxPercentage,
        });
    }

    private ActionResult HandleException(Exception exception)
    {
        switch (exception.GetType().Name)
        {
            case "NotFoundException":
                return NotFound(exception.Message);
            case "BadRequestException":
                return BadRequest(exception.Message);
            default:
                return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
