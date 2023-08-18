using Application.Features.Roles.Commands.Create;
using Application.Features.Roles.Queries.GetList;
using Infrastructure.Persistence.EntityFramework.Contexts;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers.Base;

namespace WebApi.Controllers.Derived;

public class RoleController : BaseController
{
    private readonly EfDbContext _context;

    public RoleController(EfDbContext context)
    {
        _context = context;
    }


    [HttpPost]
    public async Task<IActionResult> Add([FromBody] RoleCreateCommand roleCreateCommand)
    {
        return Created("", await Mediator.Send(roleCreateCommand));
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromBody] RoleGetListQuery roleGetListQuery)
    {
        var x = await Mediator.Send(roleGetListQuery);
        return Ok(x);
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
}