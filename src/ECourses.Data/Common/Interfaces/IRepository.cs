using ECourses.Data.Common.Enums;

namespace ECourses.Data.Common.Interfaces
{
    public interface IRepository<T> where T : Entity
    {
        Task<T?> GetById(Guid id);
        Task<IEnumerable<T>> GetAll();
        Task<RepositoryOperationResult> Create(T entity);
        Task<RepositoryOperationResult> Update(Guid id, T entity);
        Task<RepositoryOperationResult> Delete(Guid id);
        Task<IEnumerable<T>> GetByField<TField>(Func<T, TField> fieldSelector);
    }
}
