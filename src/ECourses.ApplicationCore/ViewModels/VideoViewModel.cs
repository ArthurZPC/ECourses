namespace ECourses.ApplicationCore.ViewModels
{
    public class VideoViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;

        public Guid CourseId { get; set; }
    }
}
