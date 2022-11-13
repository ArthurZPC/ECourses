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
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ECoursesDbContext _context;

        public AuthorRepository(ECoursesDbContext context)
        {
            _context = context;
        }

        public async Task Create(Author entity)
        {
            entity.Id = Guid.NewGuid();

            _context.Authors.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var authorToDelete = await _context.Authors.FirstAsync(a => a.Id == id);

            _context.Authors.Remove(authorToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<PagedList<Author>> GetByCondition(Expression<Func<Author, bool>> predicate, PaginationOptions? paginationOptions = default)
        {
            return await _context.Authors
                .Include(a => a.User)
                .Include(a => a.Courses)
                .AsNoTracking()
                .Where(predicate)
                .ToPagedListAsync(paginationOptions);
        }

        public async Task<Author?> GetById(Guid id)
        {
            return await _context.Authors
                .Include(a => a.User)
                .Include(a => a.Courses)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<PagedList<Author>> GetPagedList(PaginationOptions? paginationOptions = default, FilterOptions<Author>? filterOptions = default, OrderOptions<Author>? orderOptions = default)
        {
            var query = _context.Authors
                .Include(a => a.User)
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

        public async Task Update(Author entity)
        {
            var author = await _context.Authors.FirstAsync(a => a.Id == entity.Id);

            author.FirstName = !string.IsNullOrEmpty(entity.FirstName) ? entity.FirstName : author.FirstName;
            author.LastName = !string.IsNullOrEmpty(entity.LastName) ? entity.LastName : author.LastName;
            author.UserId = entity.UserId != Guid.Empty ? entity.UserId : author.UserId;

            await _context.SaveChangesAsync();
        }
    }
}
