using ECourses.ApplicationCore.Common.Exceptions;
using ECourses.ApplicationCore.Common.Interfaces.Validators;
using ECourses.ApplicationCore.Extensions;
using ECourses.ApplicationCore.Features.Commands.Videos;

namespace ECourses.ApplicationCore.Validators
{
    public class VideoValidator : IVideoValidator
    {
        public void ValidateCreateVideoCommand(CreateVideoCommand command)
        {
            if (command is null)
            {
                throw new ViewModelValidationException(Resources.Common.Validation_Null.F(typeof(CreateVideoCommand).Name));
            }

            if (command.Video is null)
            {
                throw new ViewModelValidationException(Resources.Common.Validation_Required.F(nameof(command.Video)));
            }

            if (string.IsNullOrEmpty(command.Title))
            {
                throw new ViewModelValidationException(Resources.Common.Validation_Required.F(nameof(command.Title)));
            }

            if (command.CourseId == Guid.Empty)
            {
                throw new ViewModelValidationException(Resources.Common.Validation_Required.F(nameof(command.CourseId)));
            }
        }

        public void ValidateUpdateVideoCommand(UpdateVideoCommand command)
        {
            if (command is null)
            {
                throw new ViewModelValidationException(Resources.Common.Validation_Null.F(typeof(UpdateVideoCommand).Name));
            }

            if (string.IsNullOrEmpty(command.Title))
            {
                throw new ViewModelValidationException(Resources.Common.Validation_Required.F(nameof(command.Title)));
            }
        }
    }
}
