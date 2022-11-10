using ECourses.ApplicationCore.ViewModels;
using MediatR;

namespace ECourses.ApplicationCore.Features.Queries.Authors
{
    public class GetAuthorQuery : IRequest<AuthorViewModel>
    {
        public Guid Id { get; set; }
    }
}
