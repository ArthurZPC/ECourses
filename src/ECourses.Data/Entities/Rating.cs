using ECourses.Data.Common;
using ECourses.Data.Identity;

namespace ECourses.Data.Entities
{
    public class Rating : Entity
    {
        public int? Value { get; set; }

        public Guid CourseId { get; set; }
        public Course Course { get; set; } = default!;

        public Guid UserId { get; set; }
        public User User { get; set; } = default!;
    }
}
