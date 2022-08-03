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
        }
    }
}
