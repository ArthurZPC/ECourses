using ECourses.Data.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECourses.Data.Entities.Configurations
{
    public class RatingEntityTypeConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.HasIndex(r => new { r.CourseId, r.UserId })
                .IsUnique();

            builder.HasOne<User>(r => r.User)
                .WithMany(u => u.Ratings)
                .OnDelete(DeleteBehavior.NoAction);
                
        }
    }
}
