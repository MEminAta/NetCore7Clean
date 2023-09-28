namespace CrossCuttingConcern.Exceptions.ExceptionTypes;

[Serializable]
public class RuleException : Exception
{
    public RuleException(string message) : base(message)
    {
    }
}