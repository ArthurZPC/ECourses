using ECourses.ApplicationCore.ViewModels;
using MediatR;

namespace ECourses.ApplicationCore.Features.Queries.Ratings
{
    public class GetRatingQuery : IRequest<RatingViewModel>
    {
        public Guid Id { get; set; }
    }
}
