using ECourses.ApplicationCore.Common.Exceptions;
using ECourses.ApplicationCore.Common.Interfaces.Validators;
using ECourses.ApplicationCore.Extensions;
using ECourses.ApplicationCore.Features.Commands.Tags;

namespace ECourses.ApplicationCore.Validators
{
    public class TagValidator : ITagValidator
    {
        public void ValidateCreateTagCommand(CreateTagCommand command)
        {
            if (command is null)
            {
                throw new ViewModelValidationException(Resources.Common.Validation_Null.F(typeof(CreateTagCommand).Name));
            }

            if (string.IsNullOrEmpty(command.Title))
            {
                throw new ViewModelValidationException(Resources.Common.Validation_Required.F(nameof(command.Title)));
            }
        }

        public void ValidateUpdateTagCommand(UpdateTagCommand command)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command), Resources.Common.Validation_Null.F(typeof(UpdateTagCommand).Name));
            }

            if (string.IsNullOrEmpty(command.Title))
            {
                throw new ViewModelValidationException(Resources.Common.Validation_Required.F(nameof(command.Title)));
            }
        }
    }
}
