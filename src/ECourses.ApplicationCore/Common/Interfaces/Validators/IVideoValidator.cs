using ECourses.ApplicationCore.Features.Commands.Videos;

namespace ECourses.ApplicationCore.Common.Interfaces.Validators
{
    public interface IVideoValidator
    {
        void ValidateCreateVideoCommand(CreateVideoCommand command);
        void ValidateUpdateVideoCommand(UpdateVideoCommand command);
    }
}
