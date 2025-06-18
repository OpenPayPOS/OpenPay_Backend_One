using OpenPay.Api.Mappers;
using OpenPay.Api.Utils;
using OpenPay.Api.V1.Models.Response;

namespace OpenPay.Api.V1.Controllers;

[ApiController]
[Route("[controller]")]
public class FinancesController : ControllerBase
{
    private readonly ExceptionHandler _exceptionHandler;
    private readonly IMapper<FinancesModel, FinancesDTO> _mapper;
    private readonly IFinancesService _service;

    public FinancesController(IFinancesService service)
    {
        _exceptionHandler = new ExceptionHandler();
        _mapper = new FinancesMapper();
        _service = service;
    }

    public async Task<ActionResult> GetFinancesAsync([FromQuery] DateTime? startTime, [FromQuery] DateTime? endTime)
    {
        var financesOptional = await _service.GetFinancesAsync(startTime, endTime);

        return await financesOptional.ProduceResultAsync(_mapper.MapDtoToModelAsync, _exceptionHandler.HandleException);
    }
}