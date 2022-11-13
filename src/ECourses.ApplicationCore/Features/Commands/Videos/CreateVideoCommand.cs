using MediatR;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ECourses.ApplicationCore.Features.Commands.Videos
{
    public class CreateVideoCommand : IRequest
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public IFormFile Video { get; set; } = default!;

        [Required]
        public Guid CourseId { get; set; }
    }
}
