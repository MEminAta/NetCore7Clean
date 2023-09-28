using Application.Features.Roles.Commands.Create;
using Application.Features.Roles.Queries.GetList;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers.Base;

namespace WebApi.Controllers.Derived;

public class RoleController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] RoleCreateCommand request, CancellationToken ct)
    {
        return Ok(await Mediator.Send(request, ct));
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromBody] RoleGetListQuery request, CancellationToken ct)
    {
        return Ok(await Mediator.Send(request, ct));
    }


//     [HttpGet("1")]
//     public async Task<IActionResult> Test1()
//     {
//         object selectedRole = "q";
//
//         for (var i = 0; i < 1000; i++)
//         {
//             selectedRole = await _context.Roles
//                 .Select("new (Id, Name)").ToDynamicListAsync();
//         }
//
//         return Ok(selectedRole);
//     }
//
//     [HttpGet("2")]
//     public async Task<IActionResult> Test2()
//     {
//         object selectedRole = "q";
//         for (var i = 0; i < 1000; i++)
//         {
//             selectedRole = await _context.Roles
//                 .Select(x => new { x.Id, x.Name }).ToListAsync();
//         }
//
//         return Ok(selectedRole);
//     }


    [HttpGet("3")]
    public IActionResult Test2()
    {
        var request = Request;
        var userAgent = request.Headers.UserAgent.ToString();
        var clientIpAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
        var deviceInfo = new
        {
            UserAgent = userAgent,
            Ip = clientIpAddress
        };

        return Ok(deviceInfo);
    }
}