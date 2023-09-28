using Security.Hash.Dto;

namespace Security.Hash;

public interface IHashHelper
{
    public HashPasswordDto CreateHash(string password);
    public byte[] CreateHash(string password, byte[] key);
}