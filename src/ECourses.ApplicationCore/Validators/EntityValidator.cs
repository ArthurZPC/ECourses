using ECourses.ApplicationCore.Exceptions;
using ECourses.ApplicationCore.Extensions;
using ECourses.ApplicationCore.Interfaces.Validators.Common;
using ECourses.Data;
using ECourses.Data.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ECourses.ApplicationCore.Validators
{
    public class EntityValidator<T> : IEntityValidator<T> where T : Entity
    {
        private readonly ECoursesDbContext _context;

        public EntityValidator(ECoursesDbContext context)
        {
            _context = context;
        }

        public virtual async Task ValidateIfEntityExistsByCondition(Expression<Func<T, bool>> predicate, string? validationMessage = null)
        {
            var entity = await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(predicate);

            if (entity is not null)
            {
                var message = validationMessage ?? Resources.Common.Validation_Exists.F(typeof(T).Name);

                throw new EntityExistsException(message);
            }
        }

        public virtual async Task ValidateIfEntityNotFoundByCondition(Expression<Func<T, bool>> predicate,  string? validationMessage = null)
        {
            var entity = await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(predicate);

            if (entity is null)
            {
                var message = validationMessage ?? Resources.Common.Validation_NotFound.F(typeof(T).Name);

                throw new EntityNotFoundException(message);
            }
        }
    }
}
