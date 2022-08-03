using ECourses.Data.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECourses.Data.Entities.Configurations
{
    public class AuthorEntityTypeConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasIndex(a => new { a.FirstName, a.LastName })
                .IsUnique();            
        }
    }
}
