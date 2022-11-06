using ECourses.Data.Common.QueryOptions;
using System.Linq.Expressions;

namespace ECourses.Data.Common.Interfaces
{
    public interface IRepository<T> where T : Entity
    {
        Task<T?> GetById(Guid id);
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(Guid id);
        Task<PagedList<T>> GetByCondition(Expression<Func<T, bool>> predicate, PaginationOptions? paginationOptions = default);
        Task<PagedList<T>> GetPagedList(PaginationOptions? paginationOptions = default, FilterOptions<T>? filterOptions = default, OrderOptions<T>? orderOptions = default);
    }
}
