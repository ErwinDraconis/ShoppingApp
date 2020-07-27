using Microsoft.AspNetCore.Mvc;
using ShoppingAPI.API.Errors;

namespace ShoppingAPI.API.Controllers
{
    [ApiController]
    [Route("error/{code}")]
    [ApiExplorerSettings(IgnoreApi = true)] // tell swagger to skip this controller
    //handle the not found resources exception
    public class ErrorController : ControllerBase
    {
        public IActionResult Error(int code)
        {
            return new ObjectResult(new ApiResponse(code));
            // then add the config in the middelware
            // app.UseStatusCodePagesWithReExecute("/error/{0}");
        }
    }
}
