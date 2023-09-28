using Application.Features.Users.Commands.Create;
using Application.Features.Users.Commands.Login;
using Application.Features.Users.Commands.RefreshToken;
using CrossCuttingConcern.AutoLog;
using CrossCuttingConcern.Globalization;
using CrossCuttingConcern.Globalization.Constants;
using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Security.Authorization;
using WebApi.Controllers.Base;

namespace WebApi.Controllers.Derived;

public class UserController : BaseController
{
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(UserCreateCommand request, CancellationToken ct)
    {
        return Ok(await Mediator.Send(request, ct));
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginCommand request, CancellationToken ct)
    {
        return Ok(await Mediator.Send(request, ct));
    }

    [HttpGet]
    [Auth(ModuleEnum.BaseModule, (int)BaseModulePermission.GetUser)]
    public IActionResult Test()
    {
        return Ok(LocalizationManager.GetString(ResourceKeys.UserLoginCommand));
    }

    [AllowAnonymous]
    [HttpPost("RefreshToken")]
    public async Task<IActionResult> RefreshToken(UserRefreshTokenCommand request, CancellationToken ct)
    {
        return Ok(await Mediator.Send(request, ct));
    }


    [HttpDelete]
    public async Task<IActionResult> Test2()
    {
        var settings = new ElasticsearchClientSettings(new Uri("https://localhost:9200"))
            .CertificateFingerprint("DC:9E:0C:90:3C:39:65:F3:A6:7B:E1:F9:15:6E:44:24:7C:98:32:47:EF:94:41:DF:12:84:01:2E:F5:69:90:79")
            .Authentication(new BasicAuthentication("elastic", "pcZUX9TBEDtwS4QRrqUT"));

        var client = new ElasticsearchClient(settings);
        var response1 = await client.Indices.CreateAsync("my_index");

        var doc1 = new Log
        {
            Id = 2,
            UserId = 0,
            Uri = "null",
            StatusCode = 1,
            RequestBody = "null",
            ResponseBody = "null",
            IsError = false,
            CreateTime = default
        };
        var response2 = await client.IndexAsync(doc1, "my_index");

        // var response3 = await client.GetAsync<Log>(1, idx => idx.Index("my_index"));
        // if (response3.IsValidResponse)
        // {
        //     var doc2 = response3.Source;
        //     return Ok(doc2);
        // }

        var response4 = await client.SearchAsync<Log>(x => x
            .Index("my-index")
            .From(0)
            .Size(10)
            .Query(y => y
                .Term(z => z.IsError, false)
            )
        );

        if (response4.IsValidResponse) return Ok(response4.Documents);

        return Ok();
    }
}


// VGJHUHZJb0JQcVdQd2wtdVY1eDk6RGwtZjdHS2RRWFMtYmpBMVh0Y0hodw== API Key
// DC:9E:0C:90:3C:39:65:F3:A6:7B:E1:F9:15:6E:44:24:7C:98:32:47:EF:94:41:DF:12:84:01:2E:F5:69:90:79 HTTP CA certificate SHA-256 fingerprint
// pcZUX9TBEDtwS4QRrqUT elastic password