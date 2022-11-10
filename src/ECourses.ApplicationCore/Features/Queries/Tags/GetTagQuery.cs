using ECourses.ApplicationCore.ViewModels;
using MediatR;

namespace ECourses.ApplicationCore.Features.Queries.Tags
{
    public class GetTagQuery : IRequest<TagViewModel>
    {
        public Guid Id { get; set; }
    }
}
