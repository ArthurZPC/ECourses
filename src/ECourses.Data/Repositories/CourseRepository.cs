using ECourses.Data.Common;
using ECourses.Data.Common.Enums;
using ECourses.Data.Common.Interfaces.Repositories;
using ECourses.Data.Common.QueryOptions;
using ECourses.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ECourses.Data.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ECoursesDbContext _context;

        public CourseRepository(ECoursesDbContext context)
        {
            _context = context;
        }

        public async Task Create(Course entity)
        {
            entity.Id = Guid.NewGuid();

            _context.Courses.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var courseToDelete = await _context.Courses.FirstAsync(c => c.Id == id);

            _context.Courses.Remove(courseToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Course>> GetAll()
        {
            return await _context.Courses
                .Include(c => c.Author)
                .ThenInclude(a => a.User)
                .Include(c => c.Category)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Course>> GetByCondition(Expression<Func<Course, bool>> predicate)
        {
            return await _context.Courses
                .Include(c => c.Author)
                .ThenInclude(a => a.User)
                .Include(c => c.Category)
                .AsNoTracking()
                .Where(predicate)
                .ToListAsync();

        }

        public async Task<Course?> GetById(Guid id)
        {
            return await _context.Courses
                .Include(c => c.Author)
                .ThenInclude(a => a.User)
                .Include(c => c.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<PagedList<Course>> GetPagedList(PaginationOptions paginationOptions, FilterOptions<Course>? filterOptions = null, OrderOptions<Course>? orderOptions = null)
        {
            var query = _context.Courses
                .Include(c => c.Author)
                .ThenInclude(a => a.User)
                .Include(c => c.Category)
                .AsNoTracking();

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

            var totalCount = await query.CountAsync();

            var items = await query.Skip(count * offset - count).Take(count).ToListAsync();

            return new PagedList<Course>
            {
                Count = totalCount,
                Items = items
            };
        }

        public async Task Update(Course entity)
        {
            _context.Courses.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
