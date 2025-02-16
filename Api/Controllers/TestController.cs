using Microsoft.AspNetCore.Mvc;

namespace OpenPay.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    [HttpGet]
    public ActionResult<string> Test()
    {
        return "Hello world!";
    }
}
