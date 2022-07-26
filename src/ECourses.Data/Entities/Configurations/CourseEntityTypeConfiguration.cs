﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECourses.Data.Entities.Configurations
{
    public class CourseEntityTypeConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasIndex(c => c.Title)
                .IsUnique();

            builder.Property(c => c.PublishedAt).IsRequired();
            builder.Property(c => c.Price).IsRequired();
        }
    }
}
