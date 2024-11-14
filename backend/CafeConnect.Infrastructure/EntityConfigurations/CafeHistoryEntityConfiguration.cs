using CafeConnect.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CafeConnect.Infrastructure.EntityConfigurations
{
    public class CafeHistoryEntityConfiguration : IEntityTypeConfiguration<CafeHistory>
    {
        public void Configure(EntityTypeBuilder<CafeHistory> builder)
        {
            builder.ToTable("Cafe_His");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.CafeId)
                .IsRequired();

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Description)
                .HasMaxLength(500);

            builder.Property(c => c.LogoId)
                   .IsRequired(false);

            builder.Property(c => c.Location)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(c => c.RowVersion)
                .IsRowVersion()
                .IsRequired();
        }
    }
}