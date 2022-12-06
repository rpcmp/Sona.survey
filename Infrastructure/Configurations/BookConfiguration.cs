using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(x => x.Id).HasName("book_id");

            builder.Property(x => x.Title)
                .HasColumnName("title")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Year)
                .HasColumnName("year");

            builder.ToTable("authors")
                .Property(x => x.Author)
                .HasColumnName("author_id")
                .IsRequired();

            builder.ToTable("genres")
                .Property(x => x.Genre)
                .HasColumnName("genre_id")
                .IsRequired();
        }
    }
}
