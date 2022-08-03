using ECourses.ApplicationCore.ViewModels;
using ECourses.ApplicationCore.ViewModels.CreateViewModels;
using ECourses.ApplicationCore.ViewModels.UpdateViewModels;
using ECourses.Data.Entities;

namespace ECourses.ApplicationCore.Common.Interfaces.Converters
{
    public interface IRatingConverter
    {
        Rating ConvertToRating(RatingViewModel model);
        Rating ConvertToRating(CreateRatingViewModel model);
        Rating ConvertToRating(UpdateRatingViewModel model);
        RatingViewModel ConvertToViewModel(Rating model);
    }
}
