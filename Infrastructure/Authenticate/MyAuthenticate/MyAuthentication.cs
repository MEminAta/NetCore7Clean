using Authentication;

namespace Infrastructure.Authenticate.MyAuthenticate;

public class MyAuthentication : IAuthentication
{
    public string Authenticate(string key)
    {
        throw new NotImplementedException();
    }

    public T Authenticate<T>(string key)
    {
        throw new NotImplementedException();
    }

    public string CreateKey(string body, DateTime exp)
    {
        throw new NotImplementedException();
    }
}