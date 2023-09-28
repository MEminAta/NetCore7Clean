namespace Application.Repository.Extensions.Paging.Models;

public class CursorPaginatedModel<T, TId>
{
    public List<T> Data { get; set; } = null!;
    public TId? FirstCursor { get; set; }
    public TId? LastCursor { get; set; }
}