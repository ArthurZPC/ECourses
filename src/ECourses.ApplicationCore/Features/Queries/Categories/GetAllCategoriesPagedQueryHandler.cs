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

namespace ECourses.ApplicationCore.Features.Queries.Categories
{
    public class GetAllCategoriesPagedQueryHandler : IRequestHandler<GetAllCategoriesPagedQuery, PagedListModel<CategoryViewModel>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategoryConverter _categoryConverter;

        public GetAllCategoriesPagedQueryHandler(ICategoryRepository categoryRepository, ICategoryConverter categoryConverter)
        {
            _categoryRepository = categoryRepository;
            _categoryConverter = categoryConverter;
        }

        public async Task<PagedListModel<CategoryViewModel>> Handle(GetAllCategoriesPagedQuery request, CancellationToken cancellationToken)
        {
            var paginationOptions = new PaginationOptions
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };

            var categoryFilterQuery = new CategoryFilterQuery()
            {
                Title = request.Title,
            };

            var filterOptions = MapFilterOptions(categoryFilterQuery);
            var orderOptions = MapOrderOptions(request.OrderField);

            var categories = await _categoryRepository.GetPagedList(paginationOptions, filterOptions, orderOptions);

            return new PagedListModel<CategoryViewModel>
            {
                Count = categories.Count,
                Items = categories.Items.Select(c => _categoryConverter.ConvertToViewModel(c))
            };
        }

        private FilterOptions<Category>? MapFilterOptions(CategoryFilterQuery? filter)
        {
            var predicate = PredicateBuilder.True<Category>();

            if (filter is null)
            {
                return null;
            }

            if (filter.Title is not null)
            {
                predicate = predicate.And((c) => c.Title.ToLower().Contains(filter.Title.ToLower()));
            }

            return new FilterOptions<Category>
            {
                Predicate = predicate
            };
        }

        private OrderOptions<Category>? MapOrderOptions(string? orderField)
        {
            return orderField switch
            {
                CategoryOrderQueryConstants.TitleAsc => new OrderOptions<Category>
                {
                    Type = OrderType.Ascending,
                    FieldSelector = c => c.Title
                },
                CategoryOrderQueryConstants.TitleDesc => new OrderOptions<Category>
                {
                    Type = OrderType.Descending,
                    FieldSelector = c => c.Title
                },
                _ => null,
            };
        }
    }
}
