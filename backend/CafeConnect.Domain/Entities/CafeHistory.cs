using System.ComponentModel.DataAnnotations.Schema;

namespace CafeConnect.Domain.Entities
{
    public class CafeHistory : BaseEntity<Guid>
    {
        public Guid CafeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? LogoId { get; set; }
        public string Location { get; set; }
        public DateTime CreatedAt { get; private set; } = DateTime.Now;

        // navigation properties
        [NotMapped]
        public ICollection<EmployeeHistory> Employees { get; set; } = [];
    }
}