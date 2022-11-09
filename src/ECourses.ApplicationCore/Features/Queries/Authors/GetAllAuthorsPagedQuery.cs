using ECourses.ApplicationCore.Common.Interfaces;
using ECourses.ApplicationCore.Models;
using ECourses.ApplicationCore.ViewModels;
using MediatR;

namespace ECourses.ApplicationCore.Features.Queries.Authors
{
    public class GetAllAuthorsPagedQuery : IRequest<PagedListModel<AuthorViewModel>>, IPaginationQuery
    {
        public int PageSize { get; set; } = 100;
        public int PageNumber { get; set; } = 1;

        public string? OrderField { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public Guid UserId { get; set; }
    }
}
