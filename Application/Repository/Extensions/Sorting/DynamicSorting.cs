using System.Linq.Dynamic.Core;

namespace Application.Repository.Extensions.Sorting;

public static class DynamicSorting
{
    public static int DynamicSort<T>(this IQueryable<T> query, List<string> props)
    {
        var x = query.Select("(id, name)");
        return 0;
    }
}