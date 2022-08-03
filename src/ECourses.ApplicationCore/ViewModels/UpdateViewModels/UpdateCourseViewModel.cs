namespace ECourses.ApplicationCore.ViewModels.UpdateViewModels
{
    public class UpdateCourseViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime? PublishedAt { get; set; }
        public decimal? Price { get; set; }

        public Guid AuthorId { get; set; }
        public Guid CategoryId { get; set; }
    }
}
