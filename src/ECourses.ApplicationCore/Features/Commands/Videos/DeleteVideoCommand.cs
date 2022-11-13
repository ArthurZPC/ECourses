using MediatR;

namespace ECourses.ApplicationCore.Features.Commands.Videos
{
    public class DeleteVideoCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
