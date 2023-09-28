namespace Security.Time;

public class TimeService
{
    public DateTime Now { get; } = DateTime.Now;
    public DateTime Initialize { get; set; } = new(2023, 1, 1);
}