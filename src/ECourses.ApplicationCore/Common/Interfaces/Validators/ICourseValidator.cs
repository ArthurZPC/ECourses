using ECourses.ApplicationCore.ViewModels.CreateViewModels;
using ECourses.ApplicationCore.ViewModels.UpdateViewModels;

namespace ECourses.ApplicationCore.Common.Interfaces.Validators
{
    public interface ICourseValidator
    {
        void ValidateCreateCourseViewModel(CreateCourseViewModel model);
        void ValidateUpdateCourseViewModel(UpdateCourseViewModel model);
    }
}
