using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers.Base;

namespace WebApi.Controllers.Derived;

public class UserController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Post()
    {
        return Ok();
    }
}