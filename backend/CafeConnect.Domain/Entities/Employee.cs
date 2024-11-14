using CafeConnect.Domain.Enums;

namespace CafeConnect.Domain.Entities
{
    public class Employee : BaseEntity<string>
    {
        public string Name { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public int PhoneNumber { get; set; }
        public GenderType Gender { get; set; } = GenderType.Male;
        public DateTime StartedAt { get; } = DateTime.Now;


        // navigation properties
        public Guid? CafeId { get; set; }
        public Cafe? Cafe { get; set; }
    }
}