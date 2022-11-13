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

        public async Task<PagedList<Video>> GetByCondition(Expression<Func<Video, bool>> predicate, PaginationOptions? paginationOptions = default)
        {
            return await _context.Videos
                .Include(v => v.Course)
                .AsNoTracking()
                .Where(predicate)
                .ToPagedListAsync(paginationOptions);
        }

        public async Task<Video?> GetById(Guid id)
        {
            return await _context.Videos
                .Include(v => v.Course)
                .AsNoTracking()
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<PagedList<Video>> GetPagedList(PaginationOptions? paginationOptions = default, FilterOptions<Video>? filterOptions = null, OrderOptions<Video>? orderOptions = null)
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

            return await query.ToPagedListAsync(paginationOptions);
        }

        public async Task Update(Video entity)
        {
            var video = await _context.Videos.FirstAsync(v => v.Id == entity.Id);

            video.Title = entity.Title != "" ? entity.Title : video.Title;
            video.Url = entity.Url != "" ? entity.Url : video.Url;
            video.CourseId = entity.CourseId != Guid.Empty ? entity.CourseId : video.CourseId;

            await _context.SaveChangesAsync();
        }
    }
}
