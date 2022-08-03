using ECourses.ApplicationCore.Common.Exceptions;
using ECourses.ApplicationCore.Common.Interfaces.Validators;
using ECourses.ApplicationCore.Extensions;
using ECourses.ApplicationCore.ViewModels.CreateViewModels;
using ECourses.ApplicationCore.ViewModels.UpdateViewModels;

namespace ECourses.ApplicationCore.Validators
{
    public class TagValidator : ITagValidator
    {
        public void ValidateCreateTagViewModel(CreateTagViewModel model)
        {
            if (model is null)
            {
                throw new ViewModelValidationException(Resources.Common.Validation_Null.F(typeof(CreateTagViewModel).Name));
            }

            if (string.IsNullOrEmpty(model.Title))
            {
                throw new ViewModelValidationException(Resources.Common.Validation_Required.F(nameof(model.Title)));
            }
        }

        public void ValidateUpdateTagViewModel(UpdateTagViewModel model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model), Resources.Common.Validation_Null.F(typeof(UpdateTagViewModel).Name));
            }

            if (string.IsNullOrEmpty(model.Title))
            {
                throw new ViewModelValidationException(Resources.Common.Validation_Required.F(nameof(model.Title)));
            }
        }
    }
}
