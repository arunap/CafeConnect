using CafeConnect.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CafeConnect.Infrastructure.EntityConfigurations
{
    public class EmployeeHistoryEntityConfiguration : IEntityTypeConfiguration<EmployeeHistory>
    {
        public void Configure(EntityTypeBuilder<EmployeeHistory> builder)
        {
            builder.ToTable("Employee_His");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.EmployeeId)
                .IsRequired();

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.EmailAddress)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.PhoneNumber)
                .HasMaxLength(8);

            builder.Property(e => e.Gender)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(e => e.StartedAt)
                .HasConversion<DateTime>()
                .IsRequired();

            builder.Property(c => c.RowVersion)
                .IsRowVersion()
                .IsRequired();

            builder
                .HasOne(e => e.Cafe)
                .WithMany(e => e.Employees)
                .HasForeignKey(e => e.CafeId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}