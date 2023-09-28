using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Infrastructure.Security.Encryption;
using Infrastructure.Security.Hash;
using Microsoft.Extensions.Configuration;
using Security.Compress;
using Security.Time;
using Security.Token;
using Security.Token.Models;

namespace Infrastructure.Security.Token.Jwt;

public class JwtTokenHelper : ITokenHelper
{
    private readonly TokenOptions _tokenOptions;

    public JwtTokenHelper(IConfiguration configuration)
    {
        _tokenOptions = configuration.GetSection(nameof(TokenOptions)).Get<TokenOptions>()!;
    }

    public string CreateAccessToken(AccessTokenModel model)
    {
        var claims = new List<Claim>
        {
            new(nameof(model.UId).ToLowerInvariant(), model.UId.ToString()),
            new(nameof(model.MId).ToLowerInvariant(), model.MId.ToString()),
            new(nameof(model.POs).ToLowerInvariant(), model.POs),
        };

        return TokenGenerator(claims, _tokenOptions.AccessTokenExpiration);
    }

    public List<AccessTokenWithModuleIdModel> CreateAccessTokens(List<ModuleIdWithPermissionOrders> model, int userId)
    {
        var accessTokens = new List<AccessTokenWithModuleIdModel>();
        foreach (var moduleIdWithPermissionIds in model)
        {
            var createAccessTokenModel = new AccessTokenModel
            {
                UId = userId,
                MId = moduleIdWithPermissionIds.ModuleId,
                POs = SortedNumberCompressor.Compress(moduleIdWithPermissionIds.PermissionOrders)
            };
            var token = CreateAccessToken(createAccessTokenModel);
            accessTokens.Add(new AccessTokenWithModuleIdModel { ModuleId = moduleIdWithPermissionIds.ModuleId, AccessToken = token });
        }

        return accessTokens;
    }

    public string CreateRefreshToken(RefreshTokenModel model)
    {
        var claims = new List<Claim>
        {
            new(nameof(model.UId).ToLowerInvariant(), model.UId.ToString()),
            new(nameof(model.Ip).ToLowerInvariant(), model.Ip),
            new(nameof(model.Ag).ToLowerInvariant(), model.Ag)
        };

        return TokenGenerator(claims, _tokenOptions.RefreshTokenExpiration);
    }

    private string TokenGenerator(IEnumerable<Claim> claims, int lifeSpan)
    {
        var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
        var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
        var timeService = new TimeService();

        JwtSecurityToken jwt = new(
            expires: timeService.Now.AddMinutes(lifeSpan),
            // notBefore: timeService.Now,
            claims: claims,
            signingCredentials: signingCredentials
        );
        JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
        return jwtSecurityTokenHandler.WriteToken(jwt);
    }
}