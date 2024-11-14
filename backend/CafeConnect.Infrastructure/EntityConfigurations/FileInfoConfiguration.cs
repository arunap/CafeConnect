using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CafeConnect.Infrastructure.EntityConfigurations
{
    public class FileInfoConfiguration : IEntityTypeConfiguration<Domain.Entities.FileInfo>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.FileInfo> builder)
        {
            builder.HasKey(f => f.Id);  // Primary key

            builder.Property(f => f.FileName)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(f => f.FilePath)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(f => f.FileSize)
                .IsRequired();

            builder.Property(f => f.UploadedAt)
                .IsRequired();

            builder.Property(f => f.ContentType)
                .HasMaxLength(100);

            builder.Property(c => c.RowVersion)
                .IsRowVersion()
                .IsRequired();
        }
    }
}