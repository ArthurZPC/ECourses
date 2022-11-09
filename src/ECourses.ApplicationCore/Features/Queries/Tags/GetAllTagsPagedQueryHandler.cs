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

namespace ECourses.ApplicationCore.Features.Queries.Tags
{
    public class GetAllTagsPagedQueryHandler : IRequestHandler<GetAllTagsPagedQuery, PagedListModel<TagViewModel>>
    {
        private readonly ITagRepository _tagRepository;
        private readonly ITagConverter _tagConverter;
        public GetAllTagsPagedQueryHandler(ITagRepository tagRepository, ITagConverter tagConverter)
        {
            _tagRepository = tagRepository;
            _tagConverter = tagConverter;
        }
        public async Task<PagedListModel<TagViewModel>> Handle(GetAllTagsPagedQuery request, CancellationToken cancellationToken)
        {
            var paginationOptions = new PaginationOptions
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };

            var tagFilterQuery = new TagFilterQuery
            {
                Title = request.Title,
            };

            var filterOptions = MapFilterOptions(tagFilterQuery);
            var orderOptions = MapOrderOptions(request.OrderField);

            var tags = await _tagRepository.GetPagedList(paginationOptions, filterOptions, orderOptions);

            return new PagedListModel<TagViewModel>
            {
                Count = tags.Count,
                Items = tags.Items.Select(t => _tagConverter.ConvertToViewModel(t))
            };
        }

        private FilterOptions<Tag>? MapFilterOptions(TagFilterQuery? filter)
        {
            var predicate = PredicateBuilder.True<Tag>();

            if (filter is null)
            {
                return null;
            }

            if (filter.Title is not null)
            {
                predicate = predicate.And(t => t.Title.ToLower().Contains(filter.Title.ToLower()));
            }

            return new FilterOptions<Tag>
            {
                Predicate = predicate
            };
        }

        private OrderOptions<Tag>? MapOrderOptions(string? orderField)
        {
            return orderField switch
            {
                TagOrderQueryConstants.TitleAsc => new OrderOptions<Tag>
                {
                    Type = OrderType.Ascending,
                    FieldSelector = t => t.Title
                },
                TagOrderQueryConstants.TitleDesc => new OrderOptions<Tag>
                {
                    Type = OrderType.Descending,
                    FieldSelector = t => t.Title
                },
                _ => null
            };
        }
    }
}
