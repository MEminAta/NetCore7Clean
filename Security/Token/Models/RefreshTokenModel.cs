namespace Security.Token.Models;

public class RefreshTokenModel
{

    public required int UId { get; set; }
    public required string Ip { get; set; }
    public required string Ag { get; set; }
}