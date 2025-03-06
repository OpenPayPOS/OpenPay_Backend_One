using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OpenPay.Api.Mappers;
using OpenPay.Api.Utils;
using OpenPay.Interfaces.Services.Common;

namespace OpenPay.Api.Controllers.Common;
public class BaseController<TDTO, TModel> : ControllerBase where TDTO : struct
{
    private readonly IBaseService<TDTO> _service;
    private readonly ILogger<BaseController<TDTO,TModel>> _logger;
    protected readonly IMapper<TModel, TDTO> _mapper;
    protected readonly ExceptionHandler _exceptionHandler;

    public BaseController(IBaseService<TDTO> service,
        ILogger<BaseController<TDTO, TModel>> logger,
        IMapper<TModel, TDTO> mapper)
    {
        _service = service;
        _logger = logger;
        _mapper = mapper;
        _exceptionHandler = new ExceptionHandler();
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TModel>> GetByIdAsync(Guid id)
    {
        if (id == Guid.Empty) return BadRequest("Id cannot be empty.");

        var itemOptional = await _service.GetByIdAsync(id);

        return await itemOptional.ProduceResultAsync(_mapper.MapDtoToModelAsync, _exceptionHandler.HandleException);
    }


    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        if (id == Guid.Empty) return BadRequest("Guid cannot be empty");
        var optional = await _service.DeleteAsync(id);

        return optional.ProduceResult(_ => NoContent(), _exceptionHandler.HandleException);
    }
}