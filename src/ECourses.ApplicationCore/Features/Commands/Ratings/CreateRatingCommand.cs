using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ECourses.ApplicationCore.Features.Commands.Ratings
{
    public class CreateRatingCommand : IRequest
    {
        [Required]
        public int Value { get; set; }

        [Required]
        public Guid CourseId { get; set; }

        [Required]
        public Guid UserId { get; set; }
    }
}
