using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TinyLibrary.Domain.Models;

namespace TinyLibrary.Infrastructure.Persistence.Configurations
{
    public class MemberConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id).HasField("id").UsePropertyAccessMode(PropertyAccessMode.Field).HasColumnName("Id");
            builder.Property(b => b.Name).HasField("name").IsRequired().UsePropertyAccessMode(PropertyAccessMode.Field).HasColumnName("Name");
            builder.Property(b => b.Email).HasField("email").IsRequired().UsePropertyAccessMode(PropertyAccessMode.Field).HasColumnName("Email");
            builder.Property(b => b.JoinedAt).HasField("joinedAt").IsRequired().UsePropertyAccessMode(PropertyAccessMode.Field).HasColumnName("JoinedAt");
            builder.Property(b => b.PenalizedUntil).HasField("penalizedUntil").UsePropertyAccessMode(PropertyAccessMode.Field).HasColumnName("PenalizedUntil");

            builder.HasIndex(b => b.Email).IsUnique();
        }
    }
}
