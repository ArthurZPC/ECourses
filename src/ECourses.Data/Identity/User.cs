using ECourses.Data.Common;
using ECourses.Data.Entities;

namespace ECourses.Data.Identity
{
    public class User : Entity
    {
        public string? Username { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;

        public string PasswordSalt { get; set; } = default!;
        public Author? Author { get; set; }

        public ICollection<Rating> Ratings = new List<Rating>();

        public ICollection<Role> Roles = new List<Role>();
    }
}
