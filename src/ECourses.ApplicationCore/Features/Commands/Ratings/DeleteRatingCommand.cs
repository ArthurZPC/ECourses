using MediatR;

namespace ECourses.ApplicationCore.Features.Commands.Ratings
{
    public class DeleteRatingCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
