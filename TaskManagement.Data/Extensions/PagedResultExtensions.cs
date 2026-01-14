using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Values.Objects;

namespace TaskManagement.Data.Extensions
{
    public static class PagedResultExtensions
    {
        public static PagedResult<T> ToPagedResult<T>(this IQueryable<T> query, int page, int size)
        where T : class
        {
            var totalCount = query.Count();

            var items = query
                .Skip((page - 1) * size)
                .Take(size)
                .ToList();

            return new PagedResult<T>
            {
                Items = items,
                Pagination = new Pagination(page, size, totalCount)
            };
        }

        public static async Task<PagedResult<T>> ToPagedResultAsync<T>(this IQueryable<T> query, int page, int size)
        where T : class
        {
            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();

            return new PagedResult<T>
            {
                Items = items,
                Pagination = new Pagination(page, size, totalCount)
            };
        }
    }
}
