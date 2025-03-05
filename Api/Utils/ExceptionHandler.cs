using Microsoft.AspNetCore.Http;

namespace OpenPay.Api.Utils;
public class ExceptionHandler : ControllerBase
{
    protected ActionResult HandleException(Exception exception)
    {
        return exception.GetType().Name switch
        {
            "NotFoundException" => NotFound(exception.Message),
            "BadRequestException" => BadRequest(exception.Message),
            _ => StatusCode(StatusCodes.Status500InternalServerError),
        };
    }
}