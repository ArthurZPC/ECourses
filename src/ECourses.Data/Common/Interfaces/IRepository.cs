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
    }
}
