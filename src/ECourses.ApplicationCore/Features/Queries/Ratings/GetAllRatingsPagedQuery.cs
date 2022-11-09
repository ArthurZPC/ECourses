using ECourses.ApplicationCore.Common.Interfaces;
using ECourses.ApplicationCore.Models;
using ECourses.ApplicationCore.ViewModels;
using MediatR;

namespace ECourses.ApplicationCore.Features.Queries.Ratings
{
    public class GetAllRatingsPagedQuery : IRequest<PagedListModel<RatingViewModel>>, IPaginationQuery
    {
        public int PageSize { get; set; } = 100;
        public int PageNumber { get; set; } = 1;

        public string? OrderField { get; set; }

        public int? Value { get; set; }

        public Guid CourseId { get; set; }
        public Guid UserId { get; set; }
    }
}
