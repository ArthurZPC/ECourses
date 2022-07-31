using System.Linq.Expressions;

namespace ECourses.Data.Common.QueryOptions
{
    public class FilterOptions<T>
    {
        public Expression<Func<T, bool>> Predicate { get; set; } = default!;
    }
}
