using ECourses.Data.Common;

namespace ECourses.Data.Entities
{
    public class Category : Entity
    {
        public string Title { get; set; } = string.Empty;

        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
