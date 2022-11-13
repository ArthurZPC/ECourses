using MediatR;

namespace ECourses.ApplicationCore.Features.Commands.Categories
{
    public class DeleteCategoryCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
