namespace Security.Token.Models;

public class AccessTokenModel
{
    public required int UId { get; set; }
    public required int MId { get; set; }
    public required string POs { get; set; }
}