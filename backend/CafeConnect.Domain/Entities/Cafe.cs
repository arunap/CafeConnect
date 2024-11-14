namespace CafeConnect.Domain.Entities
{
    public class Cafe : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? LogoId { get; set; }
        public FileInfo LogoInfo { get; set; }
        public string Location { get; set; }

        // navigation properties
        public ICollection<Employee> Employees { get; set; } = [];
    }
}