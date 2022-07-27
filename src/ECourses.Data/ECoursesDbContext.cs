using ECourses.Data.Entities;
using ECourses.Data.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ECourses.Data
{
    public class ECoursesDbContext : IdentityDbContext<User, Role, Guid>
    {
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Rating> Ratings => Set<Rating>();
        public DbSet<Tag> Tags => Set<Tag>();
        public DbSet<Author> Authors => Set<Author>();
        public DbSet<Video> Videos => Set<Video>();

        public ECoursesDbContext(DbContextOptions<ECoursesDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(ECoursesDbContext).Assembly);
            base.OnModelCreating(builder);
        }
    }
}
