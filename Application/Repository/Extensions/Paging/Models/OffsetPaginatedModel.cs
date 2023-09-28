namespace Application.Repository.Extensions.Paging.Models;

public class OffsetPaginatedModel<T>
{
    public List<T> Data { get; set; } = null!;
    public bool HasPrevious { get; set; }
    public bool HasNext { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPage { get; set; }
}