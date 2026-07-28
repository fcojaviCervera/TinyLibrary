using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TinyLibrary.Domain.Models;

namespace TinyLibrary.Infrastructure.Persistence.Configurations
{
    public class LoanConfiguration : IEntityTypeConfiguration<Loan>
    {
        public void Configure(EntityTypeBuilder<Loan> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id).HasField("id").UsePropertyAccessMode(PropertyAccessMode.Field).HasColumnName("Id");
            builder.Property(b => b.MemberId).HasField("memberId").UsePropertyAccessMode(PropertyAccessMode.Field).HasColumnName("MemberId");
            builder.Property(b => b.BookId).HasField("bookId").UsePropertyAccessMode(PropertyAccessMode.Field).HasColumnName("BookId");
            builder.Property(b => b.LoanedAt).HasField("loanedAt").UsePropertyAccessMode(PropertyAccessMode.Field).HasColumnName("LoanedAt");
            builder.Property(b => b.DueAt).HasField("dueAt").UsePropertyAccessMode(PropertyAccessMode.Field).HasColumnName("DueAt");
            builder.Property(b => b.ReturnedAt).HasField("returnedAt").UsePropertyAccessMode(PropertyAccessMode.Field).HasColumnName("ReturnedAt");
        }
    }
}
