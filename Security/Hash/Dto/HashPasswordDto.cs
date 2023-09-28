namespace Security.Hash.Dto;

public class HashPasswordDto
{
    public required byte[] Password { get; set; }
    public required byte[] Salt { get; set; }
}