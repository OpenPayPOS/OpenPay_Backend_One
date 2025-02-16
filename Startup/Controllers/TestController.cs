using Microsoft.AspNetCore.Mvc;

namespace Startup.Controllers;

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
