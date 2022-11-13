using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ECourses.ApplicationCore.Features.Commands.Categories
{
    public class CreateCategoryCommand : IRequest
    {
        [Required]
        public string Title { get; set; } = string.Empty;
    }
}
