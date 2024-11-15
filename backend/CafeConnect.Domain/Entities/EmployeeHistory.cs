using System.ComponentModel.DataAnnotations.Schema;
using CafeConnect.Domain.Enums;

namespace CafeConnect.Domain.Entities
{
    public class EmployeeHistory : BaseEntity<Guid>
    {
        public string EmployeeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public int PhoneNumber { get; set; }
        public GenderType Gender { get; set; } = GenderType.Male;
        public DateTime StartedAt { get; set; } = DateTime.Now;
        public DateTime CreatedAt { get; private set; } = DateTime.Now;


        // navigation properties
        [NotMapped]
        public Guid? CafeId { get; set; }
    }
}