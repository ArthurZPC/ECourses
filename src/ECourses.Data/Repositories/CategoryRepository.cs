using ECourses.Data.Common.Enums;
using ECourses.Data.Common.Interfaces;
using ECourses.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ECourses.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ECoursesDbContext _context;

        public CategoryRepository(ECoursesDbContext context)
        {
            _context = context;
        }

        public async Task<RepositoryOperationResult> Create(Category entity)
        {
            entity.Id = Guid.NewGuid();

            _context.Categories.Add(entity);

            var entries = await _context.SaveChangesAsync();

            return entries == 0 ? RepositoryOperationResult.Failure : RepositoryOperationResult.Success;
        }

        public async Task<RepositoryOperationResult> Delete(Guid id)
        {
            var categoryToDelete = _context.Categories.FirstOrDefault(c => c.Id == id);

            if (categoryToDelete is null)
            {
                return RepositoryOperationResult.Failure;
            }

            _context.Categories.Remove(categoryToDelete);

            var entries = await _context.SaveChangesAsync();

            return entries == 0 ? RepositoryOperationResult.Failure : RepositoryOperationResult.Success;
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _context.Categories
                .Include(c => c.Courses)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetByCondition(Expression<Func<Category, bool>> selector)
        {
            return await _context.Categories
                .Include(c => c.Courses)
                .AsNoTracking()
                .Where(selector)
                .ToListAsync();
        }

        public async Task<Category?> GetById(Guid id)
        {
            return await _context.Categories
                .Include(c => c.Courses)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<RepositoryOperationResult> Update(Category entity)
        {
            var categoryToUpdate = await _context.Categories.FirstOrDefaultAsync(c => c.Id == entity.Id);

            if (categoryToUpdate is null)
            {
                return RepositoryOperationResult.Failure;
            }

            _context.Categories.Update(entity);

            var entries = await _context.SaveChangesAsync();

            return entries == 0 ? RepositoryOperationResult.Failure : RepositoryOperationResult.Success;
        }
    }
}
