using ECourses.ApplicationCore.Common.Exceptions;
using ECourses.ApplicationCore.Extensions;
using ECourses.ApplicationCore.Common.Interfaces.Validators;
using ECourses.ApplicationCore.Features.Commands.Categories;

namespace ECourses.ApplicationCore.Validators
{
    public class CategoryValidator : ICategoryValidator
    {
        public void ValidateCreateCategoryCommand(CreateCategoryCommand command)
        {
            if (command is null) 
            {
                throw new ViewModelValidationException(Resources.Common.Validation_Null.F(typeof(CreateCategoryCommand).Name));
            }

            if (string.IsNullOrEmpty(command.Title))
            {            
                throw new ViewModelValidationException(Resources.Common.Validation_Required.F(nameof(command.Title)));
            }
        }

        public void ValidateUpdateCategoryCommand(UpdateCategoryCommand command)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command), Resources.Common.Validation_Null.F(typeof(UpdateCategoryCommand).Name));
            }

            if (string.IsNullOrEmpty(command.Title))
            {
                throw new ViewModelValidationException(Resources.Common.Validation_Required.F(nameof(command.Title)));
            }
        }
    }
}
