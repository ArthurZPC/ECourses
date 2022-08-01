using ECourses.Data.Common;
using System.Linq.Expressions;

namespace ECourses.ApplicationCore.Common.Interfaces.Validators
{
    public interface IEntityValidator<T> where T : Entity
    {
        public Task ValidateIfEntityExistsByCondition(Expression<Func<T, bool>> predicate, string? validationMessage = null);
        public Task ValidateIfEntityNotFoundByCondition(Expression<Func<T, bool>> predicate, string? validationMessage = null);
    }
}
