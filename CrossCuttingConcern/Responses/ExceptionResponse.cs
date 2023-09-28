namespace CrossCuttingConcern.Responses;

public class ExceptionResponse
{
    public required string Type { get; set; }
    public required string Details { get; set; }
    public required List<string> Errors { get; set; }
}