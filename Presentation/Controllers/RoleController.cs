using Application.Features.Roles.Commands.Create;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class RoleController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateRoleCommand createRoleCommand)
    {
        var result = await Mediator.Send(createRoleCommand);
        return Created("", result);
    }
}