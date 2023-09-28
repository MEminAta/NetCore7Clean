namespace Security.Token.Models;

public class AccessTokenWithModuleIdModel
{
    public required int ModuleId { get; set; }
    public required string AccessToken { get; set; }
}