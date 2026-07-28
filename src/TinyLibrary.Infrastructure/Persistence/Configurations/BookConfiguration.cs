using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TinyLibrary.Domain.Models;

namespace TinyLibrary.Infrastructure.Persistence.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id).HasField("id").UsePropertyAccessMode(PropertyAccessMode.Field).HasColumnName("Id");
            builder.Property(b => b.Title).HasField("title").IsRequired().UsePropertyAccessMode(PropertyAccessMode.Field).HasColumnName("Title");
            builder.Property(b => b.Author).HasField("author").IsRequired().UsePropertyAccessMode(PropertyAccessMode.Field).HasColumnName("Author");
            builder.Property(b => b.ISBN).HasField("iSBN").IsRequired().UsePropertyAccessMode(PropertyAccessMode.Field).HasColumnName("Isbn");
            builder.Property(b => b.TotalCopies).HasField("totalCopies").UsePropertyAccessMode(PropertyAccessMode.Field).HasColumnName("TotalCopies");
            builder.Property(b => b.AvailableCopies).HasField("availableCopies").UsePropertyAccessMode(PropertyAccessMode.Field).HasColumnName("AvailableCopies");

            builder.HasIndex(b => b.ISBN).IsUnique();

        }
    }
}
