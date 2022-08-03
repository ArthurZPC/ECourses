using ECourses.Data.Common;
using ECourses.Data.Common.Enums;
using ECourses.Data.Common.Interfaces.Repositories;
using ECourses.Data.Common.QueryOptions;
using ECourses.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ECourses.Data.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private readonly ECoursesDbContext _context;

        public RatingRepository(ECoursesDbContext context)
        {
            _context = context;
        }

        public async Task Create(Rating entity)
        {
            entity.Id = Guid.NewGuid();

            _context.Ratings.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var rating = await _context.Ratings.FirstAsync(r => r.Id == id);

            _context.Ratings.Remove(rating);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Rating>> GetAll()
        {
            return await _context.Ratings
                .Include(r => r.Course)
                .Include(r => r.User)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Rating>> GetByCondition(Expression<Func<Rating, bool>> predicate)
        {
            return await _context.Ratings
                .Include(r => r.Course)
                .Include(r => r.User)
                .AsNoTracking()
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<Rating?> GetById(Guid id)
        {
            return await _context.Ratings
                .Include(r => r.Course)
                .Include(r => r.User)
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<PagedList<Rating>> GetPagedList(PaginationOptions paginationOptions, FilterOptions<Rating>? filterOptions = null, OrderOptions<Rating>? orderOptions = null)
        {
            var query = _context.Ratings
                .Include(r => r.Course)
                .Include(r => r.User)
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

            return new PagedList<Rating>
            {
                Count = totalCount,
                Items = items
            };
        }

        public async Task Update(Rating entity)
        {
            _context.Ratings.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
