namespace Application.PipelineBehaviors;

[Serializable]
public class BusinessException : Exception
{
    public BusinessException(string message) : base(message)
    {
    }
}