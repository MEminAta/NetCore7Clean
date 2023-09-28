using Application.Repository.Extensions.Paging.Models;
using Domain.Bases;
using Microsoft.EntityFrameworkCore;

namespace Application.Repository.Extensions.Paging;

public static class CursorPaging
{
    public static async Task<CursorPaginatedModel<T, int?>> ToCursorPaginate<T>(this IQueryable<T> query, int pageSize, int? cursor) where T : BaseEntity<int>
    {
        if (pageSize < 0)
        {
            var data = await query.Where(x => x.Id > cursor).OrderBy(x => x).Take(pageSize).ToListAsync();
            return new CursorPaginatedModel<T, int?>
            {
                Data = data,
                FirstCursor = data.Count > 0 ? data[0].Id : null,
                LastCursor = data.Count == pageSize ? data[pageSize - 1].Id : null
            };
        }
        else
        {
            var data = await query.Where(x => x.Id < cursor).OrderBy(x => x).Take(pageSize).ToListAsync();
            return new CursorPaginatedModel<T, int?>
            {
                Data = data,
                FirstCursor = data.Count > 0 ? data[0].Id : null,
                LastCursor = data.Count == pageSize ? data[pageSize - 1].Id : null
            };
        }
    }

    public static async Task<CursorPaginatedModel<T, Guid?>> ToCursorPaginate<T>(this IQueryable<T> query, int pageSize, DateTime? cursor) where T : BaseEntity<Guid>
    {
        if (pageSize < 0)
        {
            var data = await query.Where(x => x.Id.CompareTo(cursor) == 1).OrderBy(x => x).Take(pageSize).ToListAsync();
            return new CursorPaginatedModel<T, Guid?>
            {
                Data = data,
                FirstCursor = data.Count > 0 ? data[0].Id : null,
                LastCursor = data.Count == pageSize ? data[pageSize - 1].Id : null
            };
        }
        else
        {
            var data = await query.Where(x => x.Id.CompareTo(cursor) == -1).OrderBy(x => x).Take(pageSize).ToListAsync();
            return new CursorPaginatedModel<T, Guid?>
            {
                Data = data,
                FirstCursor = data.Count > 0 ? data[0].Id : null,
                LastCursor = data.Count == pageSize ? data[pageSize - 1].Id : null
            };
        }
    }
}