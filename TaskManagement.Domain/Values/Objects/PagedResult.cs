using Mapster;

namespace TaskManagement.Domain.Values.Objects
{
    public interface IPagedResult<out T>
    {
        IReadOnlyList<T> Items { get; }
        Pagination Pagination { get; }
    }

    public class PagedResult<T> : IPagedResult<T>
    {
        public List<T> Items { get; set; } = [];
        public Pagination Pagination { get; set; } = new();

        IReadOnlyList<T> IPagedResult<T>.Items => Items;
    }

    public static class PagedResultConversion
    {
        public static PagedResult<TDest> ToPagedResult<TSource, TDest>(this PagedResult<TSource> source, Func<TSource, TDest> converter)
        where TSource : class
        where TDest : class
        {
            return new PagedResult<TDest>
            {
                Items = source.Items.Select(converter).ToList(),
                Pagination = source.Pagination
            };
        }

        public static PagedResult<TDest> ToPagedResult<TDest>(this IPagedResult<object> source)
        where TDest : class
        {
            return new PagedResult<TDest>
            {
                Items = source.Items.Adapt<List<TDest>>(),
                Pagination = source.Pagination
            };
        }

        public static PagedResult<TDest> ToPagedResult<TSource, TDest>(this PagedResult<TSource> source, List<TDest> items)
        where TSource : class
        where TDest : class
        {
            return new PagedResult<TDest>
            {
                Items = items,
                Pagination = source.Pagination
            };
        }
    }
}
