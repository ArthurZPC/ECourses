using ECourses.Data.Common.Enums;
using ECourses.Data.Common.Interfaces.Repositories;
using ECourses.Data.Common.QueryOptions;
using ECourses.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ECourses.Data.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly ECoursesDbContext _context;

        public TagRepository(ECoursesDbContext context)
        {
            _context = context;
        }

        public async Task Create(Tag entity)
        {
            entity.Id = Guid.NewGuid();

            _context.Tags.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var tagToDelete = await _context.Tags.FirstAsync(t => t.Id == id);

            _context.Tags.Remove(tagToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Tag>> GetAll()
        {
            return await _context.Tags
                .Include(t => t.Courses)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Tag>> GetByCondition(Expression<Func<Tag, bool>> predicate)
        {
            return await _context.Tags
                .Include(t => t.Courses)
                .AsNoTracking()
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<Tag?> GetById(Guid id)
        {
            return await _context.Tags
                .Include(t => t.Courses)
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Tag>> GetPagedList(PaginationOptions paginationOptions, FilterOptions<Tag>? filterOptions = null, OrderOptions<Tag>? orderOptions = null)
        {
            var query = _context.Tags.AsNoTracking();

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

        public async Task Update(Tag entity)
        {
            _context.Tags.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
