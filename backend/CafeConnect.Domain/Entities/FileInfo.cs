namespace CafeConnect.Domain.Entities
{
    public class FileInfo : BaseEntity<Guid>
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public long FileSize { get; set; }
        public string ContentType { get; set; }
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

        // navigation props for cafe
        public Cafe? Cafe { get; set; }
    }
}