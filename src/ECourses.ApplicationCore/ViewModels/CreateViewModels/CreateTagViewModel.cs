using System.ComponentModel.DataAnnotations;

namespace ECourses.ApplicationCore.ViewModels.CreateViewModels
{
    public class CreateTagViewModel
    {
        [Required]
        public string Title { get; set; } = string.Empty;
    }
}
