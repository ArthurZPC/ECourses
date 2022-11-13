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

        public async Task<PagedList<Tag>> GetByCondition(Expression<Func<Tag, bool>> predicate, PaginationOptions? paginationOptions = default)
        {
            return await _context.Tags
                .Include(t => t.Courses)
                .AsNoTracking()
                .Where(predicate)
                .ToPagedListAsync(paginationOptions);
        }

        public async Task<Tag?> GetById(Guid id)
        {
            return await _context.Tags
                .Include(t => t.Courses)
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<PagedList<Tag>> GetPagedList(PaginationOptions? paginationOptions = default, FilterOptions<Tag>? filterOptions = null, OrderOptions<Tag>? orderOptions = null)
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

            return await query.ToPagedListAsync(paginationOptions);
        }

        public async Task Update(Tag entity)
        {
            var tag = await _context.Tags.FirstAsync(t => t.Id == entity.Id);

            tag.Title = entity.Title != "" ? entity.Title : tag.Title;

            await _context.SaveChangesAsync();
        }
    }
}
