using ECourses.ApplicationCore.Common.Exceptions;
using ECourses.ApplicationCore.Common.Interfaces.Validators;
using ECourses.ApplicationCore.Extensions;
using ECourses.ApplicationCore.Features.Commands.Courses;

namespace ECourses.ApplicationCore.Validators
{
    public class CourseValidator : ICourseValidator
    {
        public void ValidateCreateCourseCommand(CreateCourseCommand command)
        {
            if (command is null)
            {
                throw new ViewModelValidationException(Resources.Common.Validation_Null.F(typeof(CreateCourseCommand).Name));
            }

            if (string.IsNullOrEmpty(command.Title))
            {
                throw new ViewModelValidationException(Resources.Common.Validation_Required.F(nameof(command.Title)));
            }

            if (command.AuthorId == Guid.Empty)
            {
                throw new ViewModelValidationException(Resources.Common.Validation_Required.F(nameof(command.AuthorId)));
            }

            if (command.CategoryId == Guid.Empty)
            {
                throw new ViewModelValidationException(Resources.Common.Validation_Required.F(nameof(command.CategoryId)));
            }
        }

        public void ValidateUpdateCourseCommand(UpdateCourseCommand command)
        {
            if (command is null)
            {
                throw new ViewModelValidationException(Resources.Common.Validation_Null.F(typeof(UpdateCourseCommand).Name));
            }
        }
    }
}
