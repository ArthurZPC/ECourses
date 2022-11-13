using MediatR;

namespace ECourses.ApplicationCore.Features.Commands.Courses
{
    public class DeleteCourseCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
