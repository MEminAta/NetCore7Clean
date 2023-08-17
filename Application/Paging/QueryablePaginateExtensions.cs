using Microsoft.EntityFrameworkCore;

namespace Application.Paging;

public static class QueryablePaginateExtensions
{
    public static async Task<IPaginate<T>> ToPaginateAsync<T>(this IQueryable<T> source, int index, int size, int from = 0, CancellationToken ct = default)
    {
        if (from > index) throw new ArgumentException($"From: {from} > Index: {index}, must from <= Index");

        var count = await source.CountAsync(ct).ConfigureAwait(false);
        var items = await source.Skip((index - from) * size).Take(size).ToListAsync(ct)
            .ConfigureAwait(false);
        Paginate<T> list = new()
        {
            Index = index,
            Size = size,
            From = from,
            Count = count,
            Items = items,
            Pages = (int)Math.Ceiling(count / (double)size)
        };
        return list;
    }
}