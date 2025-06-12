//using OpenPay.Api.V2.Models.Response;

//namespace OpenPay.Api.V2.Controllers;
//[ApiController]
//[Route("v2/[controller]")]
//public class ItemsController : ControllerBase
//{
//    [HttpGet]
//    public IAsyncEnumerable<ItemResponse> GetAllAsync()
//    {
//        throw new NotImplementedException();
//    }

//    [HttpGet("category/{id}")]
//    public IAsyncEnumerable<ItemResponse> GetByCategoryIdAsync(Guid categoryId) { throw new NotImplementedException(); }

//    [HttpGet("filter/{filter}")]
//    public IAsyncEnumerable<ItemResponse> FindByFilterAsync(string filter) { throw new NotImplementedException(); }

//    [HttpGet("invalid")]
//    public IAsyncEnumerable<ItemResponse> GetByInvalidAsync() { throw new NotImplementedException(); }

//    [HttpGet("{id}")]
//    public Task<ActionResult<ItemResponse>> GetByIdAsync(Guid id) { throw new NotImplementedException(); }

//    [HttpPost]
//    public Task<ActionResult<ItemResponse>> CreateItemAsync(CreateItemRequest createItem) { throw new NotImplementedException(); }

//    [HttpPut]
//    public Task<ActionResult<ItemResponse>> EditItemAsync(EditItemRequest editItem) { throw new NotImplementedException(); }

//    [HttpDelete]
//    public Task<ActionResult<string>> DeleteItemAsync(Guid id) { throw new NotImplementedException(); }

//    [HttpDelete]
//    public Task<IActionResult> ConfirmDeleteItemAsync(Guid id, string confirmationToken) { throw new NotImplementedException(); }
//}