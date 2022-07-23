using ECourses.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace ECourses.Data.Identity
{
    public class User : IdentityUser<Guid>
    {
        public Guid? AuthorId { get; set; }
        public Author? Author { get; set; }
    }
}
