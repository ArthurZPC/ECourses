using ECourses.Data.Common.Enums;
using ECourses.Data.Common.Interfaces.Repositories;
using ECourses.Data.Common.QueryOptions;
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

        public async Task Create(Category entity)
        {
            entity.Id = Guid.NewGuid();

            _context.Categories.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var categoryToDelete = await _context.Categories.FirstAsync(c => c.Id == id);

            _context.Categories.Remove(categoryToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _context.Categories
                .Include(c => c.Courses)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetByCondition(Expression<Func<Category, bool>> predicate)
        {
            return await _context.Categories
                .Include(c => c.Courses)
                .AsNoTracking()
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<Category?> GetById(Guid id)
        {
            return await _context.Categories
                .Include(c => c.Courses)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Category>> GetPagedList(PaginationOptions paginationOptions, FilterOptions<Category>? filterOptions = null, OrderOptions<Category>? orderOptions = null)
        {
            var query = _context.Categories.AsNoTracking();

            if (filterOptions is not null)
            {
                query = query.Where(filterOptions.Predicate);
            }

            if (orderOptions is not null)
            {
                var selector = orderOptions.FieldSelector;

                query = orderOptions.Type == OrderType.Ascending ? 
                    query.OrderBy(selector) 
                    : query.OrderByDescending(selector);
            }

            var count = paginationOptions.PageSize;
            var offset = paginationOptions.PageNumber;

            return await query.Skip(count * offset - count).Take(count).ToListAsync();
        }

        public async Task Update(Category entity)
        {
            _context.Categories.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
