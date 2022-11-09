using ECourses.ApplicationCore.Common.Interfaces;
using ECourses.ApplicationCore.Models;
using ECourses.ApplicationCore.ViewModels;
using MediatR;

namespace ECourses.ApplicationCore.Features.Queries.Tags
{
    public class GetAllTagsPagedQuery : IRequest<PagedListModel<TagViewModel>>, IPaginationQuery
    {
        public int PageSize { get; set; } = 100;
        public int PageNumber { get; set; } = 1;

        public string? OrderField { get; set; }

        public string? Title { get; set; }
    }
}
