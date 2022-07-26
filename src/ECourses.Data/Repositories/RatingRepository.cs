﻿using ECourses.Data.Common;
using ECourses.Data.Common.Enums;
using ECourses.Data.Common.Interfaces.Repositories;
using ECourses.Data.Common.QueryOptions;
using ECourses.Data.Entities;
using ECourses.Data.Extensions;
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

        public async Task<PagedList<Rating>> GetByCondition(Expression<Func<Rating, bool>> predicate, PaginationOptions? paginationOptions = default)
        {
            return await _context.Ratings
                .Include(r => r.Course)
                .Include(r => r.User)
                .AsNoTracking()
                .Where(predicate)
                .ToPagedListAsync(paginationOptions);
        }

        public async Task<Rating?> GetById(Guid id)
        {
            return await _context.Ratings
                .Include(r => r.Course)
                .Include(r => r.User)
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<PagedList<Rating>> GetPagedList(PaginationOptions? paginationOptions = default, FilterOptions<Rating>? filterOptions = null, OrderOptions<Rating>? orderOptions = null)
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

            return await query.ToPagedListAsync(paginationOptions);
        }

        public async Task Update(Rating entity)
        {
            var rating = await _context.Ratings.FirstAsync(r => r.Id == entity.Id);

            rating.Value = entity.Value != null ? entity.Value : rating.Value;
            rating.CourseId = entity.CourseId != Guid.Empty ? entity.CourseId : rating.CourseId;
            rating.UserId = entity.UserId != Guid.Empty ? entity.UserId : rating.UserId;

            await _context.SaveChangesAsync();
        }
    }
}
