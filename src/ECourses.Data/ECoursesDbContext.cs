using ECourses.Data.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ECourses.Data
{
    public class ECoursesDbContext : IdentityDbContext<User, Role, Guid>
    {
        public ECoursesDbContext(DbContextOptions<ECoursesDbContext> options) : base(options) { }
    }
}
