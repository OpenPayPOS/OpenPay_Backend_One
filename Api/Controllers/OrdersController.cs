using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OpenPay.Api.Models.Request;
using OpenPay.Api.Models.Response;
using OpenPay.Interfaces.Services;
using OpenPay.Interfaces.Services.ServiceModels;
using OpenPay.Api.Mappers;
using OpenPay.Api.Controllers.Common;

namespace OpenPay.Api.Controllers;
[ApiController]
[Route("api/v1/[controller]")]
public class OrdersController : BaseController<OrderDTO, OrderResponse>
{
    private readonly IOrderService _orderService;
    private readonly ILogger<OrdersController> _logger;

    public OrdersController(IOrderService orderService, ILogger<OrdersController> logger)
        : base(orderService, logger, new OrderMapper())
    {
        _orderService = orderService;
        _logger = logger;
    }

    [HttpPost]
    [ProducesResponseType<OrderResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<OrderResponse>> CreateAsync([FromBody] CreateOrderRequest order)
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
            return CreatedAtAction(nameof(GetByIdAsync), new { id = itemDTO.Id }, await _mapper.MapDtoToModelAsync(itemDTO));
        }, _exceptionHandler.HandleException);
    }
}