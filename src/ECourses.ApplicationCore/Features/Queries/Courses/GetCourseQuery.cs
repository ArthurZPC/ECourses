using ECourses.ApplicationCore.ViewModels;
using MediatR;

namespace ECourses.ApplicationCore.Features.Queries.Courses
{
    public class GetCourseQuery : IRequest<CourseViewModel>
    {
        public Guid Id { get; set; }
    }
}
