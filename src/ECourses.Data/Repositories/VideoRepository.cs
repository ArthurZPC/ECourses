using ECourses.Data.Common;
using ECourses.Data.Common.Enums;
using ECourses.Data.Common.Interfaces.Repositories;
using ECourses.Data.Common.QueryOptions;
using ECourses.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ECourses.Data.Repositories
{
    public class VideoRepository : IVideoRepository
    {
        private readonly ECoursesDbContext _context;

        public VideoRepository(ECoursesDbContext context)
        {
            _context = context;
        }

        public async Task Create(Video entity)
        {
            entity.Id = Guid.NewGuid();

            _context.Videos.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var video = await _context.Videos.FirstAsync(x => x.Id == id);

            _context.Videos.Remove(video);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Video>> GetAll()
        {
            return await _context.Videos
                .Include(v => v.Course)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Video>> GetByCondition(Expression<Func<Video, bool>> predicate)
        {
            return await _context.Videos
                .Include(v => v.Course)
                .AsNoTracking()
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<Video?> GetById(Guid id)
        {
            return await _context.Videos
                .Include(v => v.Course)
                .AsNoTracking()
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<PagedList<Video>> GetPagedList(PaginationOptions paginationOptions, FilterOptions<Video>? filterOptions = null, OrderOptions<Video>? orderOptions = null)
        {
            var query = _context.Videos
                .Include(v => v.Course)
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

            return new PagedList<Video>
            {
                Count = totalCount,
                Items = items
            };
        }

        public async Task Update(Video entity)
        {
            _context.Videos.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
