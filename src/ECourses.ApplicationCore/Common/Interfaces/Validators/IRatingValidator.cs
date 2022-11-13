using ECourses.ApplicationCore.Features.Commands.Ratings;

namespace ECourses.ApplicationCore.Common.Interfaces.Validators
{
    public interface IRatingValidator
    {
        void ValidateCreateRatingCommand(CreateRatingCommand command);
        void ValidateUpdateRatingCommand(UpdateRatingCommand command);
    }
}
