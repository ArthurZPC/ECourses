using ECourses.ApplicationCore.Common.Exceptions;
using ECourses.ApplicationCore.Common.Interfaces.Validators;
using ECourses.ApplicationCore.Extensions;
using ECourses.ApplicationCore.ViewModels.CreateViewModels;
using ECourses.ApplicationCore.ViewModels.UpdateViewModels;

namespace ECourses.ApplicationCore.Validators
{
    public class RatingValidator : IRatingValidator
    {
        public void ValidateCreateRatingViewModel(CreateRatingViewModel model)
        {
            if (model is null)
            {
                throw new ViewModelValidationException(Resources.Common.Validation_Null.F(typeof(CreateRatingViewModel).Name));
            }

            if (model.CourseId == Guid.Empty)
            {
                throw new ViewModelValidationException(Resources.Common.Validation_Required.F(nameof(model.CourseId)));
            }

            if (model.UserId == Guid.Empty)
            {
                throw new ViewModelValidationException(Resources.Common.Validation_Required.F(nameof(model.UserId)));
            }
        }

        public void ValidateUpdateRatingViewModel(UpdateRatingViewModel model)
        {
            if (model is null)
            {
                throw new ViewModelValidationException(Resources.Common.Validation_Null.F(typeof(UpdateRatingViewModel).Name));
            }
        }
    }
}
