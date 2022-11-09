using ECourses.ApplicationCore.Common.Interfaces;
using ECourses.ApplicationCore.Models;
using ECourses.ApplicationCore.ViewModels;
using MediatR;

namespace ECourses.ApplicationCore.Features.Queries.Courses
{
    public class GetAllCoursesPagedQuery : IRequest<PagedListModel<CourseViewModel>>, IPaginationQuery
    {
        public int PageSize { get; set; } = 100;
        public int PageNumber { get; set; } = 1;

        public string? OrderField { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

        public DateTime? PublishedAt { get; set; }

        public decimal? PriceMin { get; set; }
        public decimal? PriceMax { get; set; }

        public Guid AuthorId { get; set; }
        public Guid CategoryId { get; set; }
    }
}
