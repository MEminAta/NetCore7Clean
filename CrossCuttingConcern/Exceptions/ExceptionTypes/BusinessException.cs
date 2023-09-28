namespace CrossCuttingConcern.Exceptions.ExceptionTypes;

public class BusinessException : Exception
{
    public BusinessException(string message) : base(message)
    {
    }
}