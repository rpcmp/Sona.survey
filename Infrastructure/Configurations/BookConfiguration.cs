using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Book");

            builder.HasKey(x => x.Id)
                .HasName("id");

            builder.Property(x => x.Title)
                .HasColumnName("title")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Year)
                .HasColumnName("year");

            builder
                .HasOne(x => x.Author)
                .WithMany(x => x.Books)
                .IsRequired();

            builder
                .HasOne(x => x.Genre)
                .WithMany(x => x.Books)
                .IsRequired();
        }
    }
}
