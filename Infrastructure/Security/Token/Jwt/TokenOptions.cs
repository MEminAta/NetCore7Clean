namespace Infrastructure.Security.Token.Jwt;

public class TokenOptions
{
    public required int AccessTokenExpiration { get; set; }
    public required int RefreshTokenExpiration { get; set; }
    public required string SecurityKey { get; set; }
}