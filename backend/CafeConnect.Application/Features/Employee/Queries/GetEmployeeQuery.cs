using CafeConnect.Application.Features.Employee.Dtos;
using CafeConnect.Domain.Repositories;
using MediatR;

namespace CafeConnect.Application.Features.Employee.Queries
{
    public class GetEmployeeQuery : IRequest<EmployeeDto>
    {
        public string Id { get; set; } = null!;
    }

    public class GetEmployeeQueryHandler : IRequestHandler<GetEmployeeQuery, EmployeeDto>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICafeRepository _cafeRepository;

        public GetEmployeeQueryHandler(IEmployeeRepository employeeRepository, ICafeRepository cafeRepository)
        {
            _employeeRepository = employeeRepository;
            _cafeRepository = cafeRepository;
        }

        public async Task<EmployeeDto> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetByIdAsync(request.Id);
            var cafe = employee == null ? null : await _cafeRepository.GetByIdAsync(employee.CafeId.Value);

            var item = new EmployeeDto
            {
                EmployeeId = employee.Id,
                Name = employee.Name,
                EmailAddress = employee.EmailAddress,
                Gender = employee.Gender,
                PhoneNumber = employee.PhoneNumber,
                CafeId = cafe?.Id,
                CafeName = cafe?.Name
            };

            return item;
        }
    }
}