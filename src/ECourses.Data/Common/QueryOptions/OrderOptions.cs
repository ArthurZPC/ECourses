using ECourses.Data.Common.Enums;
using System.Linq.Expressions;

namespace ECourses.Data.Common.QueryOptions
{
    public class OrderOptions<T>
    {
        public OrderType Type { get; set; }

        public Expression<Func<T, object>> FieldSelector { get; set; } = default!;
    }
}
