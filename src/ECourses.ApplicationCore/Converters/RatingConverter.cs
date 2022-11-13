using ECourses.ApplicationCore.Common.Interfaces.Converters;
using ECourses.ApplicationCore.Features.Commands.Ratings;
using ECourses.ApplicationCore.ViewModels;
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

        public Rating ConvertToRating(CreateRatingCommand command)
        {
            return new Rating
            {
                Value = command.Value,
                CourseId = command.CourseId,
                UserId = command.UserId
            };
        }

        public Rating ConvertToRating(UpdateRatingCommand command)
        {
            return new Rating
            {
                Id = command.Id,
                Value = command.Value,
                CourseId = command.CourseId,
                UserId = command.UserId
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
