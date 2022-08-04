using Microsoft.AspNetCore.Http;

namespace ECourses.ApplicationCore.ViewModels.CreateViewModels
{
    public class CreateVideoViewModel
    {
        public string Title { get; set; } = string.Empty;

        public IFormFile Video { get; set; } = default!;

        public Guid CourseId { get; set; }
    }
}
