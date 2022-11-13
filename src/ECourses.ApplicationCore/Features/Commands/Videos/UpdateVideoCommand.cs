using MediatR;
using Microsoft.AspNetCore.Http;

namespace ECourses.ApplicationCore.Features.Commands.Videos
{
    public class UpdateVideoCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;

        public IFormFile Video { get; set; } = default!;

        public string NewUrl { get; set; } = string.Empty;

        public Guid CourseId { get; set; }
    }
}
