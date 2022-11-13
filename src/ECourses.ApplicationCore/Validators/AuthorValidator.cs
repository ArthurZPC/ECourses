using ECourses.ApplicationCore.Common.Exceptions;
using ECourses.ApplicationCore.Common.Interfaces.Validators;
using ECourses.ApplicationCore.Extensions;
using ECourses.ApplicationCore.Features.Commands.Authors;

namespace ECourses.ApplicationCore.Validators
{
    public class AuthorValidator : IAuthorValidator
    {
        public void ValidateCreateAuthorCommand(CreateAuthorCommand command)
        {
            if (command is null)
            {
                throw new ViewModelValidationException(Resources.Common.Validation_Null.F(typeof(CreateAuthorCommand).Name));
            }

            if (string.IsNullOrEmpty(command.FirstName))
            {
                throw new ViewModelValidationException(Resources.Common.Validation_Required.F(nameof(command.FirstName)));
            }

            if (string.IsNullOrEmpty(command.LastName))
            {
                throw new ViewModelValidationException(Resources.Common.Validation_Required.F(nameof(command.LastName)));
            }

            if (command.UserId == Guid.Empty)
            {
                throw new ViewModelValidationException(Resources.Common.Validation_Required.F(nameof(command.UserId)));
            }
        }

        public void ValidateUpdateAuthorCommand(UpdateAuthorCommand command)
        {
            if (command is null)
            {
                throw new ViewModelValidationException(Resources.Common.Validation_Null.F(typeof(UpdateAuthorCommand).Name));
            }
        }
    }
}
