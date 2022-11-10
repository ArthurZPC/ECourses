using ECourses.ApplicationCore.ViewModels;
using MediatR;

namespace ECourses.ApplicationCore.Features.Queries.Videos
{
    public class GetVideoQuery : IRequest<VideoViewModel>
    {
        public Guid Id { get; set; }
    }
}
