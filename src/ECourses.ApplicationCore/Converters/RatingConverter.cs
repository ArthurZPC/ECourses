using ECourses.ApplicationCore.Common.Interfaces.Converters;
using ECourses.ApplicationCore.ViewModels;
using ECourses.ApplicationCore.ViewModels.CreateViewModels;
using ECourses.ApplicationCore.ViewModels.UpdateViewModels;
using ECourses.Data.Entities;

namespace ECourses.ApplicationCore.Converters
{
    public class RatingConverter : IRatingConverter
    {
        public Rating ConvertToRating(RatingViewModel model)
        {
            return new Rating
            {
                Id = model.Id,
                Value = model.Value,
                CourseId = model.CourseId,
                UserId = model.UserId
            };
        }

        public Rating ConvertToRating(CreateRatingViewModel model)
        {
            return new Rating
            {
                Value = model.Value,
                CourseId = model.CourseId,
                UserId = model.UserId
            };
        }

        public Rating ConvertToRating(UpdateRatingViewModel model)
        {
            return new Rating
            {
                Id = model.Id,
                Value = model.Value ?? 0,
                CourseId = model.CourseId,
                UserId = model.UserId
            };
        }

        public RatingViewModel ConvertToViewModel(Rating model)
        {
            return new RatingViewModel
            {
                Id = model.Id,
                Value = model.Value,
                CourseId = model.CourseId,
                UserId = model.UserId
            };
        }
    }
}
