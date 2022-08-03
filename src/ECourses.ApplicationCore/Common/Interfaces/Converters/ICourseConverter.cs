using ECourses.ApplicationCore.ViewModels;
using ECourses.ApplicationCore.ViewModels.CreateViewModels;
using ECourses.ApplicationCore.ViewModels.UpdateViewModels;
using ECourses.Data.Entities;

namespace ECourses.ApplicationCore.Common.Interfaces.Converters
{
    public interface ICourseConverter
    {
        Course ConvertToCourse(CourseViewModel model);
        Course ConvertToCourse(CreateCourseViewModel model);
        Course ConvertToCourse(UpdateCourseViewModel model);
        CourseViewModel ConvertToViewModel(Course model);
    }
}
