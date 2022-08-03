using System.ComponentModel.DataAnnotations;

namespace ECourses.ApplicationCore.ViewModels.CreateViewModels
{
    public class CreateAuthorViewModel
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public Guid UserId { get; set; }
    }
}
