using Application.Repository.Extensions.Paging.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Repository.Extensions.Paging;

public static class OffsetPaging
{
    public static async Task<OffsetPaginatedModel<T>> ToOffsetPaginate<T>(this IQueryable<T> query, int pageSize, int page)
    {
        var totalPage = await query.CountAsync() / pageSize;
        var data = await query.OrderBy(x => x).Skip(pageSize * (page - 1)).Take(pageSize).ToListAsync();
        return new OffsetPaginatedModel<T>
        {
            Data = data,
            HasNext = page != 1,
            HasPrevious = page != totalPage,
            Page = page,
            PageSize = pageSize,
            TotalPage = totalPage
        };
    }
}