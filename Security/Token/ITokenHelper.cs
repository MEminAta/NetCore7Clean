using Security.Token.Models;

namespace Security.Token;

public interface ITokenHelper
{
    public string CreateAccessToken(AccessTokenModel model);
    public List<AccessTokenWithModuleIdModel> CreateAccessTokens(List<ModuleIdWithPermissionOrders> model, int userId);
    public string CreateRefreshToken(RefreshTokenModel model);
}