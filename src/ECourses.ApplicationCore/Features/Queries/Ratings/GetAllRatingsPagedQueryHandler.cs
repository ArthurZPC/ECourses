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

namespace ECourses.ApplicationCore.Features.Queries.Ratings
{
    public class GetAllRatingsPagedQueryHandler : IRequestHandler<GetAllRatingsPagedQuery, PagedListModel<RatingViewModel>>
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly IRatingConverter _ratingConverter;

        public GetAllRatingsPagedQueryHandler(IRatingRepository ratingRepository, IRatingConverter ratingConverter)
        {
            _ratingRepository = ratingRepository;
            _ratingConverter = ratingConverter;
        }
        public async Task<PagedListModel<RatingViewModel>> Handle(GetAllRatingsPagedQuery request, CancellationToken cancellationToken)
        {
            var paginationOptions = new PaginationOptions
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };

            var ratingFilterQuery = new RatingFilterQuery()
            {
                Value = request.Value,
                CourseId = request.CourseId,
                UserId = request.UserId
            };

            var filterOptions = MapFilterOptions(ratingFilterQuery);
            var orderOptions = MapOrderOptions(request.OrderField);

            var ratings = await _ratingRepository.GetPagedList(paginationOptions, filterOptions, orderOptions);

            return new PagedListModel<RatingViewModel>
            {
                Count = ratings.Count,
                Items = ratings.Items.Select(r => _ratingConverter.ConvertToViewModel(r))
            };
        }

        private FilterOptions<Rating>? MapFilterOptions(RatingFilterQuery? filter)
        {
            var predicate = PredicateBuilder.True<Rating>();

            if (filter is null)
            {
                return null;
            }

            if (filter.Value is not null)
            {
                predicate = predicate.And(r => r.Value == filter.Value);
            }

            if (filter.CourseId != Guid.Empty)
            {
                predicate = predicate.And(r => r.CourseId == filter.CourseId);
            }

            if (filter.UserId != Guid.Empty)
            {
                predicate = predicate.And(r => r.UserId == filter.UserId);
            }

            return new FilterOptions<Rating>
            {
                Predicate = predicate
            };
        }

        private OrderOptions<Rating>? MapOrderOptions(string? orderField)
        {
            return orderField switch
            {
                RatingOrderQueryConstants.ValueAsc => new OrderOptions<Rating>
                {
                    Type = OrderType.Ascending,
                    FieldSelector = r => r.Value
                },
                RatingOrderQueryConstants.ValueDesc => new OrderOptions<Rating>
                {
                    Type = OrderType.Descending,
                    FieldSelector = r => r.Value
                },
                _ => null
            };
        }
    }
}
