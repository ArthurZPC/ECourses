using System.ComponentModel.DataAnnotations;

namespace ECourses.ApplicationCore.ViewModels.UpdateViewModels
{
    public class UpdateAuthorViewModel
    {
        [Required]
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public Guid UserId { get; set; }
    }
}
