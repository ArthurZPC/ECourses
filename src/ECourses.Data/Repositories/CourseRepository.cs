using ECourses.Data.Common;
using ECourses.Data.Common.Enums;
using ECourses.Data.Common.Interfaces.Repositories;
using ECourses.Data.Common.QueryOptions;
using ECourses.Data.Entities;
using ECourses.Data.Extensions;
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

        public async Task<PagedList<Course>> GetByCondition(Expression<Func<Course, bool>> predicate, PaginationOptions? paginationOptions = default)
        {
            return await _context.Courses
                .Include(c => c.Author)
                .ThenInclude(a => a.User)
                .Include(c => c.Category)
                .AsNoTracking()
                .Where(predicate)
                .ToPagedListAsync(paginationOptions);

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

        public async Task<PagedList<Course>> GetPagedList(PaginationOptions? paginationOptions = default, FilterOptions<Course>? filterOptions = null, OrderOptions<Course>? orderOptions = null)
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

            return await query.ToPagedListAsync(paginationOptions);
        }

        public async Task Update(Course entity)
        {
            var course = await _context.Courses.FirstAsync(c => c.Id == entity.Id);

            course.Title = !string.IsNullOrEmpty(entity.Title) ? entity.Title : course.Title;
            course.Description = !string.IsNullOrEmpty(entity.Description) ? entity.Description : course.Description;
            course.PublishedAt = entity.PublishedAt != null ? entity.PublishedAt : course.PublishedAt;
            course.Price = entity.Price != null ? entity.Price : course.Price;
            course.AuthorId = entity.AuthorId != Guid.Empty ? entity.AuthorId : course.AuthorId;
            course.CategoryId = entity.CategoryId != Guid.Empty ? entity.CategoryId : course.CategoryId;

            await _context.SaveChangesAsync();
        }
    }
}
