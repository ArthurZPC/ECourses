using ECourses.ApplicationCore.Common.Exceptions;
using ECourses.ApplicationCore.Common.Interfaces.Validators;
using ECourses.ApplicationCore.Extensions;
using ECourses.ApplicationCore.ViewModels.CreateViewModels;
using ECourses.ApplicationCore.ViewModels.UpdateViewModels;

namespace ECourses.ApplicationCore.Validators
{
    public class AuthorValidator : IAuthorValidator
    {
        public void ValidateCreateAuthorViewModel(CreateAuthorViewModel model)
        {
            if (model is null)
            {
                throw new ViewModelValidationException(Resources.Common.Validation_Null.F(typeof(CreateAuthorViewModel).Name));
            }

            if (string.IsNullOrEmpty(model.FirstName))
            {
                throw new ViewModelValidationException(Resources.Common.Validation_Required.F(nameof(model.FirstName)));
            }

            if (string.IsNullOrEmpty(model.LastName))
            {
                throw new ViewModelValidationException(Resources.Common.Validation_Required.F(nameof(model.LastName)));
            }

            if (model.UserId == Guid.Empty)
            {
                throw new ViewModelValidationException(Resources.Common.Validation_Required.F(nameof(model.UserId)));
            }
        }

        public void ValidateUpdateAuthorViewModel(UpdateAuthorViewModel model)
        {
            if (model is null)
            {
                throw new ViewModelValidationException(Resources.Common.Validation_Null.F(typeof(UpdateAuthorViewModel).Name));
            }
        }
    }
}
