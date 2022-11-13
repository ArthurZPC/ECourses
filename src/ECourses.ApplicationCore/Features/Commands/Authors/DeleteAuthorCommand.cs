using MediatR;

namespace ECourses.ApplicationCore.Features.Commands.Authors
{
    public class DeleteAuthorCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
