namespace Application.Responses;

public class ExceptionResponse
{
    public string Type { get; set; }
    public string Details { get; set; }
    public List<string> Errors { get; set; }

    public ExceptionResponse(string type, string details, List<string> errors)
    {
        Type = type;
        Details = details;
        Errors = errors;
    }
}