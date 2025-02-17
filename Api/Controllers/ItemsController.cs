namespace OpenPay.Api.Controllers;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenPay.Api.Models.Request;
using OpenPay.Api.Models.Response;

[Route("api/v1/[controller]")]
[ApiController]
public class ItemsController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async IAsyncEnumerable<ItemResponse> GetAllAsync()
    {
        throw new NotImplementedException();
        yield return new ItemResponse();
# warning implement this method
    }

    [HttpGet("{id}")]
    [ProducesResponseType<ItemResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ItemResponse>> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
# warning implement this method
    }

    [HttpPost]
    [ProducesResponseType<ItemResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ItemResponse>> CreateAsync([FromBody] CreateItemRequest item)
    {
        throw new NotImplementedException();
#warning implement this method
    }

    [HttpPatch]
    [ProducesResponseType<ItemResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ItemResponse>> EditAsync([FromBody] EditItemRequest item)
    {
        throw new NotImplementedException();
#warning implement this method
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
#warning implement this method
    }
}
