using ECourses.ApplicationCore.Common.Exceptions;
using ECourses.ApplicationCore.Common.Interfaces.Validators;
using ECourses.ApplicationCore.Extensions;
using ECourses.ApplicationCore.Features.Commands.Ratings;

namespace ECourses.ApplicationCore.Validators
{
    public class RatingValidator : IRatingValidator
    {
        public void ValidateCreateRatingCommand(CreateRatingCommand command)
        {
            if (command is null)
            {
                throw new ViewModelValidationException(Resources.Common.Validation_Null.F(typeof(CreateRatingCommand).Name));
            }

            if (command.CourseId == Guid.Empty)
            {
                throw new ViewModelValidationException(Resources.Common.Validation_Required.F(nameof(command.CourseId)));
            }

            if (command.UserId == Guid.Empty)
            {
                throw new ViewModelValidationException(Resources.Common.Validation_Required.F(nameof(command.UserId)));
            }
        }

        public void ValidateUpdateRatingCommand(UpdateRatingCommand command)
        {
            if (command is null)
            {
                throw new ViewModelValidationException(Resources.Common.Validation_Null.F(typeof(UpdateRatingCommand).Name));
            }
        }
    }
}
