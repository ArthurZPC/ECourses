using ECourses.Data.Common.Enums;
using System.Linq.Expressions;

namespace ECourses.Data.Common.Interfaces
{
    public interface IRepository<T> where T : Entity
    {
        Task<T?> GetById(Guid id);
        Task<IEnumerable<T>> GetAll();
        Task<RepositoryOperationResult> Create(T entity);
        Task<RepositoryOperationResult> Update(Guid id, T entity);
        Task<RepositoryOperationResult> Delete(Guid id);
        Task<IEnumerable<T>> GetByCondition(Expression<Func<T, bool>> selector);
    }
}
