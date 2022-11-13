using ECourses.ApplicationCore.Features.Commands.Courses;

namespace ECourses.ApplicationCore.Common.Interfaces.Validators
{
    public interface ICourseValidator
    {
        void ValidateCreateCourseCommand(CreateCourseCommand command);
        void ValidateUpdateCourseCommand(UpdateCourseCommand command);
    }
}
