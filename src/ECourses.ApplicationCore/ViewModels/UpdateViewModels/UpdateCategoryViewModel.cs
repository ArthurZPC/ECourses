using System.ComponentModel.DataAnnotations;

namespace ECourses.ApplicationCore.ViewModels.UpdateViewModels
{
    public class UpdateCategoryViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;
    }
}