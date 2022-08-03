using ECourses.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace ECourses.Data.Identity
{
    public class User : IdentityUser<Guid>
    {
        public Author? Author { get; set; }

        public ICollection<Rating> Ratings = new List<Rating>();
    }
}
