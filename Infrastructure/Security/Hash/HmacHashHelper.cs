using System.Security.Cryptography;
using System.Text;
using Security.Hash;
using Security.Hash.Dto;

namespace Infrastructure.Security.Hash;

public class HmacHashHelper : IHashHelper
{
    public HashPasswordDto CreateHash(string password)
    {
        var hmac = new HMACSHA256();
        var salt = hmac.Key;
        var hashedPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

        return new HashPasswordDto { Salt = salt, Password = hashedPassword };
    }

    public byte[] CreateHash(string password, byte[] key)
    {
        var hmac = new HMACSHA256(key);
        var hashedPassword = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return hashedPassword;
    }
}