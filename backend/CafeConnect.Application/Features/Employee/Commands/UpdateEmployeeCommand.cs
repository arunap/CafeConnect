using CafeConnect.Domain.Enums;
using CafeConnect.Domain.Repositories;
using MediatR;

namespace CafeConnect.Application.Features.Employee.Commands
{
    public class UpdateEmployeeCommand : IRequest
    {
        public string EmployeeId { get; set; }
        public Guid? CafeId { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public int PhoneNumber { get; set; }
        public GenderType Gender { get; set; }
    }

    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public UpdateEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = new Domain.Entities.Employee
            {
                Id = request.EmployeeId,
                EmailAddress = request.EmailAddress,
                PhoneNumber = request.PhoneNumber,
                Gender = request.Gender,
                Name = request.Name,
                CafeId = (request.CafeId.HasValue && request.CafeId != Guid.Empty) ? request.CafeId : null,
            };

            await _employeeRepository.UpdateAsync(employee);
        }
    }
}