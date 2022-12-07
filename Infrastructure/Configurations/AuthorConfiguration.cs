using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.ToTable("Author");

            builder.HasKey(x => x.Id)
                .HasName("author_id");

            builder.Property(x => x.FirstName)
                .HasColumnName("first_name")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.LastName)
                .HasColumnName("last_name")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Birthdate)
                .HasColumnName("birthdate");

            builder
                .HasMany(x => x.Books)
                .WithOne(x => x.Author)
                .HasPrincipalKey(x => x.Id)
                .HasForeignKey(x => x.Author.Id);
        }
    }
}
