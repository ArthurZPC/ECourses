using ECourses.Data.Common.QueryOptions;
using System.Linq.Expressions;

namespace ECourses.Data.Common.Interfaces
{
    public interface IRepository<T> where T : Entity
    {
        Task<T?> GetById(Guid id);
        Task<IEnumerable<T>> GetAll();
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(Guid id);
        Task<IEnumerable<T>> GetByCondition(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetPagedList(PaginationOptions paginationOptions, FilterOptions<T>? filterOptions = null, OrderOptions<T>? orderOptions = null);
    }
}
