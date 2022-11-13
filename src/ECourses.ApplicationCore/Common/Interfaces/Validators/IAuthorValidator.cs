using ECourses.ApplicationCore.Features.Commands.Authors;

namespace ECourses.ApplicationCore.Common.Interfaces.Validators
{
    public interface IAuthorValidator
    {
        void ValidateCreateAuthorCommand(CreateAuthorCommand command);
        void ValidateUpdateAuthorCommand(UpdateAuthorCommand command);
    }
}
