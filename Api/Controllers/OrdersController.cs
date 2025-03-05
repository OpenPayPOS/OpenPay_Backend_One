using OpenPay.Api.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OpenPay.Api.Models.Request;
using OpenPay.Api.Models.Response;
using OpenPay.Interfaces.Services;
using OpenPay.Interfaces.Services.ServiceModels;
using OpenPay.Api.Mappers;

namespace OpenPay.Api.Controllers;
[ApiController]
[Route("api/v1/[controller]")]
public class OrdersController : ExceptionHandler
{
    private readonly IOrderService _orderService;
    private readonly ILogger<OrdersController> _logger;

    public OrdersController(IOrderService orderService, ILogger<OrdersController> logger)
    {
        _orderService = orderService;
        _logger = logger;
    }

    [HttpGet("{id}")]
    [ProducesResponseType<OrderResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<OrderResponse>> GetByIdAsync(Guid id)
    {
        if (id == Guid.Empty) return BadRequest("Id cannot be empty.");

        var itemOptional = await _orderService.GetByIdAsync(id);

        return await itemOptional.ProduceResultAsync(OrderMapper.MapDtoToModelAsync, HandleException);
    }

    [HttpPost]
    [ProducesResponseType<OrderResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<OrderResponse>> CreateAsync([FromForm] CreateOrderRequest order)
    {
        List<CreateOrderItemDTO> items = new List<CreateOrderItemDTO>();

        foreach (var orderItem in order.OrderItems)
        {
            items.Add(new CreateOrderItemDTO
            {
                Amount = (decimal)orderItem.Amount,
                ItemId = (Guid)orderItem.ItemId
            });
        }

        var itemOptional = await _orderService.CreateAsync(items, order.CreatedTime);

        return await itemOptional.ProduceResultAsync(async itemDTO =>
        {
            return CreatedAtAction(nameof(GetByIdAsync), new { id = itemDTO.Id }, await OrderMapper.MapDtoToModelAsync(itemDTO));
        }, HandleException);
    }
}