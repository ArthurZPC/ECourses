using ECourses.ApplicationCore.Features.Commands.Ratings;
using ECourses.ApplicationCore.ViewModels;
using ECourses.Data.Entities;

namespace ECourses.ApplicationCore.Common.Interfaces.Converters
{
    public interface IRatingConverter
    {
        Rating ConvertToRating(RatingViewModel model);
        Rating ConvertToRating(CreateRatingCommand command);
        Rating ConvertToRating(UpdateRatingCommand command);
        RatingViewModel ConvertToViewModel(Rating model);
    }
}
