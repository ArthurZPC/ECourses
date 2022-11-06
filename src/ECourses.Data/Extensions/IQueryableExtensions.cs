using ECourses.Data.Common;
using ECourses.Data.Common.QueryOptions;
using Microsoft.EntityFrameworkCore;

namespace ECourses.Data.Extensions
{
    public static class IQueryableExtensions
    {
        public static async Task<PagedList<T>> ToPagedListAsync<T>(this IQueryable<T> query, PaginationOptions? paginationOptions = default)
        {
            int count;
            int offset;

            if (paginationOptions is null)
            {
                var defaultPaginationOptions = new PaginationOptions();

                count = defaultPaginationOptions.PageSize;
                offset = defaultPaginationOptions.PageNumber;
            } 
            else
            {
                count = paginationOptions.PageSize;
                offset = paginationOptions.PageNumber;
            }

            var totalCount = await query.CountAsync();

            var items = await query.Skip(count * offset - count).Take(count).ToListAsync();

            return new PagedList<T>
            {
                Count = totalCount,
                Items = items
            };
        }
    }
}
