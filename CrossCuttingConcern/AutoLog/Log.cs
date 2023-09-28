namespace CrossCuttingConcern.AutoLog;

public class Log
{
    public required int Id { get; set; }
    public required int UserId { get; set; }

    public required string Uri { get; set; }
    public required int StatusCode { get; set; }

    public required string RequestBody { get; set; }
    public required string ResponseBody { get; set; }

    public required bool IsError { get; set; }

    public required DateTime CreateTime { get; set; }
}