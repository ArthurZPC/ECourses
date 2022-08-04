using Microsoft.AspNetCore.Http;

namespace ECourses.ApplicationCore.ViewModels.UpdateViewModels
{
    public class UpdateVideoViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;

        public IFormFile Video { get; set; } = default!;

        public Guid CourseId { get; set; }
    }
}
