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

namespace ECourses.ApplicationCore.Features.Queries.Authors
{
    public class GetAllAuthorsPagedQueryHandler : IRequestHandler<GetAllAuthorsPagedQuery, PagedListModel<AuthorViewModel>>
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IAuthorConverter _authorConverter;

        public GetAllAuthorsPagedQueryHandler(IAuthorRepository authorRepository, IAuthorConverter authorConverter)
        {
            _authorRepository = authorRepository;
            _authorConverter = authorConverter;
        }

        public async Task<PagedListModel<AuthorViewModel>> Handle(GetAllAuthorsPagedQuery request, CancellationToken cancellationToken)
        {
            var paginationOptions = new PaginationOptions
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };

            var authorFilterQuery = new AuthorFilterQuery
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserId = request.UserId,
            };

            var filterOptions = MapFilterOptions(authorFilterQuery);
            var orderOptions = MapOrderOptions(request.OrderField);

            var authors = await _authorRepository.GetPagedList(paginationOptions, filterOptions, orderOptions);

            return new PagedListModel<AuthorViewModel>
            {
                Count = authors.Count,
                Items = authors.Items.Select(a => _authorConverter.ConvertToViewModel(a))
            };
        }

        private FilterOptions<Author>? MapFilterOptions(AuthorFilterQuery? filter)
        {
            var predicate = PredicateBuilder.True<Author>();

            if (filter is null)
            {
                return null;
            }

            if (filter.FirstName is not null)
            {
                predicate = predicate.And(a => a.FirstName.ToLower().Contains(filter.FirstName.ToLower()));
            }

            if (filter.LastName is not null)
            {
                predicate = predicate.And(a => a.LastName.ToLower().Contains(filter.LastName.ToLower()));
            }

            if (filter.UserId != Guid.Empty)
            {
                predicate = predicate.And(a => a.UserId == filter.UserId);
            }

            return new FilterOptions<Author>
            {
                Predicate = predicate
            };
        }

        private OrderOptions<Author>? MapOrderOptions(string? orderField)
        {
            return orderField switch
            {
                AuthorOrderQueryConstants.FirstNameAsc => new OrderOptions<Author>
                {
                    Type = OrderType.Ascending,
                    FieldSelector = a => a.FirstName
                },
                AuthorOrderQueryConstants.FirstNameDesc => new OrderOptions<Author>
                {
                    Type = OrderType.Descending,
                    FieldSelector = a => a.FirstName
                },

                AuthorOrderQueryConstants.LastNameAsc => new OrderOptions<Author>
                {
                    Type = OrderType.Ascending,
                    FieldSelector = a => a.LastName
                },
                AuthorOrderQueryConstants.LastNameDesc => new OrderOptions<Author>
                {
                    Type = OrderType.Descending,
                    FieldSelector = a => a.LastName
                },
                _ => null
            };
        }
    }
}
