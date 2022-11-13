using ECourses.ApplicationCore.Features.Commands.Tags;

namespace ECourses.ApplicationCore.Common.Interfaces.Validators
{
    public interface ITagValidator
    {
        void ValidateCreateTagCommand(CreateTagCommand command);
        void ValidateUpdateTagCommand(UpdateTagCommand command);
    }
}
