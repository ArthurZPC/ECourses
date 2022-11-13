using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ECourses.ApplicationCore.Features.Commands.Tags
{
    public class UpdateTagCommand : IRequest
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;
    }
}
