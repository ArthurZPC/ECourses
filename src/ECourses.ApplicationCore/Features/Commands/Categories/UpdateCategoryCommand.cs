using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ECourses.ApplicationCore.Features.Commands.Categories
{
    public class UpdateCategoryCommand : IRequest
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;
    }
}
