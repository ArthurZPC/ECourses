using ECourses.Data.Entities;
using ECourses.Data.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECourses.Data
{
    public class ECoursesDbContext : DbContext
    {
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Rating> Ratings => Set<Rating>();
        public DbSet<Tag> Tags => Set<Tag>();
        public DbSet<Author> Authors => Set<Author>();
        public DbSet<Video> Videos => Set<Video>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();

        public ECoursesDbContext(DbContextOptions<ECoursesDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(ECoursesDbContext).Assembly);
            base.OnModelCreating(builder);
        }
    }
}
