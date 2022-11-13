using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ECourses.ApplicationCore.Features.Commands.Authors
{
    public class UpdateAuthorCommand : IRequest
    {
        [Required]
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public Guid UserId { get; set; }
    }
}
