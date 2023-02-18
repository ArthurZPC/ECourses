using ECourses.ApplicationCore.Common.Constants;
using ECourses.ApplicationCore.Common.Interfaces.Converters;
using ECourses.ApplicationCore.Helpers;
using ECourses.ApplicationCore.Models;
using ECourses.ApplicationCore.ViewModels;
using ECourses.ApplicationCore.WebQueries.Filters;
using ECourses.Data.Common.Enums;
using ECourses.Data.Common.Interfaces.Repositories;
using ECourses.Data.Common.QueryOptions;
using ECourses.Data.Entities;
using MediatR;

namespace ECourses.ApplicationCore.Features.Queries.Courses
{
    public class GetAllCoursesPagedQueryHandler : IRequestHandler<GetAllCoursesPagedQuery, PagedListModel<CourseViewModel>>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ICourseConverter _courseConverter;

        public GetAllCoursesPagedQueryHandler(ICourseRepository courseRepository, ICourseConverter courseConverter)
        {
            _courseRepository = courseRepository;
            _courseConverter = courseConverter;
        }


        public async Task<PagedListModel<CourseViewModel>> Handle(GetAllCoursesPagedQuery request, CancellationToken cancellationToken)
        {
            var paginationOptions = new PaginationOptions
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };

            var courseFilterQuery = new CourseFilterQuery()
            {
                Title = request.Title,
                Description = request.Description,
                PublishedAt = request.PublishedAt,
                PriceMin = request.PriceMin,
                PriceMax = request.PriceMax,
                AuthorId = request.AuthorId,
                CategoryId = request.CategoryId
            };

            var filterOptions = MapFilterOptions(courseFilterQuery);
            var orderOptions = MapOrderOptions(request.OrderField);

            var categories = await _courseRepository.GetPagedList(paginationOptions, filterOptions, orderOptions);

            return new PagedListModel<CourseViewModel>
            {
                Count = categories.Count,
                Items = categories.Items.Select(c => _courseConverter.ConvertToViewModel(c))
            };
        }

        private FilterOptions<Course>? MapFilterOptions(CourseFilterQuery? filter)
        {
            var predicate = PredicateBuilder.True<Course>();

            if (filter is null)
            {
                return null;
            }

            if (filter.Title is not null)
            {
                predicate = predicate.And(c => c.Title.ToLower().Contains(filter.Title.ToLower()));
            }

            if (filter.Description is not null)
            {
                predicate = predicate.And(c => c.Description.ToLower().Contains(filter.Description.ToLower()));
            }

            if (filter.CategoryId != Guid.Empty)
            {
                predicate = predicate.And(c => c.CategoryId == filter.CategoryId);
            }

            if (filter.AuthorId != Guid.Empty)
            {
                predicate = predicate.And(c => c.AuthorId == filter.AuthorId);
            }

            if (filter.PublishedAt is not null)
            {
                predicate = predicate.And(c => c.PublishedAt == filter.PublishedAt);
            }

            if (filter.PriceMin is not null)
            {
                predicate = predicate.And(c => c.Price >= filter.PriceMin);
            }

            if (filter.PriceMax is not null)
            {
                predicate = predicate.And(c => c.Price <= filter.PriceMax);
            }

            return new FilterOptions<Course>
            {
                Predicate = predicate
            };
        }

        private OrderOptions<Course>? MapOrderOptions(string? orderField)
        {
            return orderField switch
            {
                CourseOrderQueryConstants.TitleAsc => new OrderOptions<Course>
                {
                    Type = OrderType.Ascending,
                    FieldSelector = c => c.Title
                },
                CourseOrderQueryConstants.TitleDesc => new OrderOptions<Course>
                {
                    Type = OrderType.Descending,
                    FieldSelector = c => c.Title
                },

                CourseOrderQueryConstants.PriceAsc => new OrderOptions<Course>
                {
                    Type = OrderType.Ascending,
                    FieldSelector = c => c.Price!
                },
                CourseOrderQueryConstants.PriceDesc => new OrderOptions<Course>
                {
                    Type = OrderType.Descending,
                    FieldSelector = c => c.Price!
                },

                CourseOrderQueryConstants.PublishedAtAsc => new OrderOptions<Course>
                {
                    Type = OrderType.Ascending,
                    FieldSelector = c => c.PublishedAt!
                },
                CourseOrderQueryConstants.PublishedAtDesc => new OrderOptions<Course>
                {
                    Type = OrderType.Descending,
                    FieldSelector = c => c.PublishedAt!
                },
                _ => null
            };
        }
    }
}
