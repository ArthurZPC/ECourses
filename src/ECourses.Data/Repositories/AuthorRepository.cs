﻿using ECourses.Data.Common;
using ECourses.Data.Common.Enums;
using ECourses.Data.Common.Interfaces.Repositories;
using ECourses.Data.Common.QueryOptions;
using ECourses.Data.Entities;
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

        public async Task<IEnumerable<Author>> GetAll()
        {
            return await _context.Authors
                .Include(a => a.User)
                .Include(a => a.Courses)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Author>> GetByCondition(Expression<Func<Author, bool>> predicate)
        {
            return await _context.Authors
                .Include(a => a.User)
                .Include(a => a.Courses)
                .AsNoTracking()
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<Author?> GetById(Guid id)
        {
            return await _context.Authors
                .Include(a => a.User)
                .Include(a => a.Courses)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<PagedList<Author>> GetPagedList(PaginationOptions paginationOptions, FilterOptions<Author>? filterOptions = null, OrderOptions<Author>? orderOptions = null)
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

            var count = paginationOptions.PageSize;
            var offset = paginationOptions.PageNumber;

            var totalCount = await query.CountAsync();

            var items = await query.Skip(count * offset - count).Take(count).ToListAsync();

            return new PagedList<Author>
            {
                Count = totalCount,
                Items = items
            };
        }

        public async Task Update(Author entity)
        {
            _context.Authors.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}