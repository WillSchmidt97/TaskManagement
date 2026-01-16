using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Values.Objects;

namespace TaskManagement.Data.Extensions
{
    public static class IncludeExtensions
    {
        public static IQueryable<T> AddInclude<T>(this IQueryable<T> query, Include? includes) where T : class
        {
            if (includes is null || !includes.Includes.Any())
                return query;

            foreach (var include in includes.Includes)
                query = query.Include(include);

            return query;
        }
    }
}
