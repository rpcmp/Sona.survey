﻿using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasKey(x => x.Id).HasName("genre_id");

            builder.Property(x => x.Name)
                .HasColumnName("name")
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
