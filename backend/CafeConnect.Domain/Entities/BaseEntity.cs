namespace CafeConnect.Domain.Entities
{
    public class BaseEntity<TKey>
    {
        public TKey Id { get; set; }
        public byte[] RowVersion { get; set; }
    }
}