using System.Text;
using CafeConnect.Domain.Enums;
using CafeConnect.Domain.Repositories;
using MediatR;

namespace CafeConnect.Application.Features.Employee.Commands
{
    public class CreateEmployeeCommand : IRequest<string>
    {
        public Guid? CafeId { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public int PhoneNumber { get; set; }
        public GenderType Gender { get; set; }
    }

    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, string>
    {
        private const string Prefix = "UI";
        private const int Length = 7;
        private const string Characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";

        private readonly IEmployeeRepository _employeeRepository;

        public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }


        public async Task<string> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            string employeeId;
            do
            {
                employeeId = GenerateEmployeeId();

            } while (await _employeeRepository.GetByIdAsync(employeeId) != null);

            var employee = new Domain.Entities.Employee
            {
                Id = employeeId,
                EmailAddress = request.EmailAddress,
                PhoneNumber = request.PhoneNumber,
                Gender = request.Gender,
                Name = request.Name,
                CafeId = (request.CafeId.HasValue && request.CafeId != Guid.Empty) ? request.CafeId : null,
            };

            var insertedId = await _employeeRepository.InsertAsync(employee);
            return insertedId;
        }

        private static string GenerateEmployeeId()
        {
            var random = new Random();
            var result = new StringBuilder();

            for (int i = 0; i < Length; i++)
                result.Append(Characters[random.Next(Characters.Length)]);

            return $"{Prefix}{result}";
        }
    }
}