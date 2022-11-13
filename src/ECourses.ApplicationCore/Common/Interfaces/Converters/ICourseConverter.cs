using ECourses.ApplicationCore.Features.Commands.Courses;
using ECourses.ApplicationCore.ViewModels;
using ECourses.Data.Entities;

namespace ECourses.ApplicationCore.Common.Interfaces.Converters
{
    public interface ICourseConverter
    {
        Course ConvertToCourse(CourseViewModel model);
        Course ConvertToCourse(CreateCourseCommand command);
        Course ConvertToCourse(UpdateCourseCommand command);
        CourseViewModel ConvertToViewModel(Course model);
    }
}
