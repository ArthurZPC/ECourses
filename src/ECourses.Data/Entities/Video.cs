using ECourses.Data.Common;

namespace ECourses.Data.Entities
{
    public class Video : Entity
    {
        public string Title { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;

        public Guid CourseId { get; set; }
        public Course Course { get; set; } = default!;

    }
}
