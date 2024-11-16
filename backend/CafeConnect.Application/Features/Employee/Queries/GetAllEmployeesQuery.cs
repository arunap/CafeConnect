using CafeConnect.Application.Features.Employee.Dtos;
using CafeConnect.Domain.Repositories;
using MediatR;

namespace CafeConnect.Application.Features.Employee.Queries
{
    public class GetAllEmployeesQuery : IRequest<IEnumerable<EmployeesByCafeNameDto>>
    {
        public string? CafeName { get; set; }
    }

    public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, IEnumerable<EmployeesByCafeNameDto>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICafeRepository _cafeRepository;

        public GetAllEmployeesQueryHandler(IEmployeeRepository employeeRepository, ICafeRepository cafeRepository)
        {
            _cafeRepository = cafeRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<EmployeesByCafeNameDto>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            // get cafes by location
            var cafes = await _cafeRepository.GetAllAsync(cafe => string.IsNullOrEmpty(request.CafeName) || cafe.Name == request.CafeName);

            // get employees by cafe
            var filteredCafeIds = cafes.Select(c => c.Id).ToList();
            var employees = await _employeeRepository.GetAllAsync(emp => string.IsNullOrEmpty(request.CafeName) || filteredCafeIds.Contains(emp.CafeId.Value));

            var query = employees.Select(employee =>
                          new EmployeesByCafeNameDto
                          {
                              EmployeeId = employee.Id,
                              Name = employee.Name,
                              EmailAddress = employee.EmailAddress,
                              Gender = employee.Gender,
                              PhoneNumber = employee.PhoneNumber,
                              DaysWorked = !cafes.Any(c => c.Id == employee.CafeId) ? 0 : (DateTime.Today - employee.StartedAt).Days,
                              CafeName = cafes.SingleOrDefault(c => c.Id == employee.CafeId)?.Name,
                              StartedAt = employee.StartedAt,
                          }).OrderByDescending(e => e.DaysWorked);

            return query.ToList();
        }
    }
}