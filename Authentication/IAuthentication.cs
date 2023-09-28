namespace Authentication;

public interface IAuthentication
{
    public string Authenticate(string key);
    public T Authenticate<T>(string key);
    public string CreateKey(string body, DateTime exp);
}