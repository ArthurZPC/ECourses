using ECourses.ApplicationCore.Common.Exceptions;
using ECourses.ApplicationCore.Common.Interfaces.Validators;
using ECourses.ApplicationCore.Extensions;
using ECourses.ApplicationCore.ViewModels.CreateViewModels;
using ECourses.ApplicationCore.ViewModels.UpdateViewModels;

namespace ECourses.ApplicationCore.Validators
{
    public class CourseValidator : ICourseValidator
    {
        public void ValidateCreateCourseViewModel(CreateCourseViewModel model)
        {
            if (model is null)
            {
                throw new ViewModelValidationException(Resources.Common.Validation_Null.F(typeof(CreateCourseViewModel).Name));
            }

            if (string.IsNullOrEmpty(model.Title))
            {
                throw new ViewModelValidationException(Resources.Common.Validation_Required.F(nameof(model.Title)));
            }

            if (model.AuthorId == Guid.Empty)
            {
                throw new ViewModelValidationException(Resources.Common.Validation_Required.F(nameof(model.AuthorId)));
            }

            if (model.CategoryId == Guid.Empty)
            {
                throw new ViewModelValidationException(Resources.Common.Validation_Required.F(nameof(model.CategoryId)));
            }
        }

        public void ValidateUpdateCourseViewModel(UpdateCourseViewModel model)
        {
            if (model is null)
            {
                throw new ViewModelValidationException(Resources.Common.Validation_Null.F(typeof(CreateCourseViewModel).Name));
            }
        }
    }
}
