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

namespace ECourses.ApplicationCore.Features.Queries.Videos
{
    public class GetAllVideosPagedQueryHandler : IRequestHandler<GetAllVideosPagedQuery, PagedListModel<VideoViewModel>>
    {
        private readonly IVideoRepository _videoRepository;
        private readonly IVideoConverter _videoConverter;

        public GetAllVideosPagedQueryHandler(IVideoRepository videoRepository, IVideoConverter videoConverter)
        {
            _videoRepository = videoRepository;
            _videoConverter = videoConverter;
        }

        public async Task<PagedListModel<VideoViewModel>> Handle(GetAllVideosPagedQuery request, CancellationToken cancellationToken)
        {
            var paginationOptions = new PaginationOptions
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };

            var videoFilterQuery = new VideoFilterQuery()
            {
                Title = request.Title,
                CourseId = request.CourseId
            };

            var filterOptions = MapFilterOptions(videoFilterQuery);
            var orderOptions = MapOrderOptions(request.OrderField);

            var videos = await _videoRepository.GetPagedList(paginationOptions, filterOptions, orderOptions);

            return new PagedListModel<VideoViewModel>
            {
                Count = videos.Count,
                Items = videos.Items.Select(v => _videoConverter.ConvertToViewModel(v))
            };
        }

        private FilterOptions<Video>? MapFilterOptions(VideoFilterQuery? filter)
        {
            var predicate = PredicateBuilder.True<Video>();

            if (filter is null)
            {
                return null;
            }

            if (filter.Title is not null)
            {
                predicate = predicate.And(v => v.Title.ToLower().Contains(filter.Title.ToLower()));
            }

            if (filter.CourseId != Guid.Empty)
            {
                predicate = predicate.And(v => v.CourseId == filter.CourseId);
            }

            return new FilterOptions<Video>
            {
                Predicate = predicate
            };
        }

        private OrderOptions<Video>? MapOrderOptions(string? orderField)
        {
            return orderField switch
            {
                VideoOrderQueryConstants.TitleAsc => new OrderOptions<Video>
                {
                    Type = OrderType.Ascending,
                    FieldSelector = v => v.Title
                },
                VideoOrderQueryConstants.TitleDesc => new OrderOptions<Video>
                {
                    Type = OrderType.Descending,
                    FieldSelector = v => v.Title
                },
                _ => null
            };
        }
    }
}
