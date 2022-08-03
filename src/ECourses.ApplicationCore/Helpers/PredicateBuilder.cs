using System.Linq.Expressions;

namespace ECourses.ApplicationCore.Helpers
{
    public static class PredicateBuilder
    {
        public static Expression<Func<T, bool>> True<T>() => (t) => true;
        public static Expression<Func<T, bool>> False<T>() => (t) => false;

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.OrElse);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.AndAlso);
        }

        private static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
        {  
            var map = first.Parameters
                .Select((f, i) => new { f, s = second.Parameters[i] })
                .ToDictionary(p => p.s, p => p.f);
 
            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);

            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }
    }
}
