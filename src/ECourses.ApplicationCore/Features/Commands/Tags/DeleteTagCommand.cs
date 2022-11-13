using MediatR;

namespace ECourses.ApplicationCore.Features.Commands.Tags
{
    public class DeleteTagCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
