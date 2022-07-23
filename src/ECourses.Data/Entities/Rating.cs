using ECourses.Data.Common;

namespace ECourses.Data.Entities
{
    public class Rating : Entity
    {
        public int Value { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; } = default!;
    }
}
