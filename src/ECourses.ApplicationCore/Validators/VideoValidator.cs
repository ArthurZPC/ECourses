using ECourses.ApplicationCore.Common.Exceptions;
using ECourses.ApplicationCore.Common.Interfaces.Validators;
using ECourses.ApplicationCore.Extensions;
using ECourses.ApplicationCore.ViewModels.CreateViewModels;
using ECourses.ApplicationCore.ViewModels.UpdateViewModels;

namespace ECourses.ApplicationCore.Validators
{
    public class VideoValidator : IVideoValidator
    {
        public void ValidateCreateVideoViewModel(CreateVideoViewModel model)
        {
            if (model is null)
            {
                throw new ViewModelValidationException(Resources.Common.Validation_Null.F(typeof(CreateVideoViewModel).Name));
            }

            if (model.Video is null)
            {
                throw new ViewModelValidationException(Resources.Common.Validation_Required.F(nameof(model.Video)));
            }

            if (string.IsNullOrEmpty(model.Title))
            {
                throw new ViewModelValidationException(Resources.Common.Validation_Required.F(nameof(model.Title)));
            }

            if (model.CourseId == Guid.Empty)
            {
                throw new ViewModelValidationException(Resources.Common.Validation_Required.F(nameof(model.CourseId)));
            }
        }

        public void ValidateUpdateVideoViewModel(UpdateVideoViewModel model)
        {
            if (model is null)
            {
                throw new ViewModelValidationException(Resources.Common.Validation_Null.F(typeof(UpdateVideoViewModel).Name));
            }

            if (string.IsNullOrEmpty(model.Title))
            {
                throw new ViewModelValidationException(Resources.Common.Validation_Required.F(nameof(model.Title)));
            }
        }
    }
}
