using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ECourses.ApplicationCore.Features.Commands.Tags
{
    public class CreateTagCommand : IRequest
    {
        [Required]
        public string Title { get; set; } = string.Empty;
    }
}
