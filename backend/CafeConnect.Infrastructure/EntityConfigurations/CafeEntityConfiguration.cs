using CafeConnect.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CafeConnect.Infrastructure.EntityConfigurations
{
    public class CafeEntityConfiguration : IEntityTypeConfiguration<Cafe>
    {
        public void Configure(EntityTypeBuilder<Cafe> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Description)
                .HasMaxLength(500);

            builder
                   .HasOne(a => a.LogoInfo)
                   .WithOne(b => b.Cafe)
                   .HasForeignKey<Cafe>(b => b.LogoId)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.Property(c => c.Location)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(c => c.RowVersion)
                .IsRowVersion()
                .IsRequired();

        }
    }
}