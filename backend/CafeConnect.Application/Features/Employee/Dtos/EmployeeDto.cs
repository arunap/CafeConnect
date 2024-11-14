using CafeConnect.Domain.Enums;

namespace CafeConnect.Application.Features.Employee.Dtos
{
    public class EmployeeDto
    {
        public string EmployeeId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string EmailAddress { get; set; } = null!;
        public int PhoneNumber { get; set; }
        public GenderType Gender { get; set; }
        public Guid? CafeId { get; set; }
        public string? CafeName { get; internal set; }
    }
}