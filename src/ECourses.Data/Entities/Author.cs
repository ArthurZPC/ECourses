using ECourses.Data.Common;
using ECourses.Data.Identity;

namespace ECourses.Data.Entities
{
    public class Author : Entity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public Guid UserId { get; set; }
        public User User { get; set; } = default!;

        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
