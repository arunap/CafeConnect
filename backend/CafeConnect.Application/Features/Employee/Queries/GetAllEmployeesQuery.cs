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
            var employees = await _employeeRepository.GetAllAsync(emp => filteredCafeIds.Any(x => x == emp.CafeId));

            var query = from employee in employees
                        join cafe in cafes on employee.CafeId equals cafe.Id into cafeGroup
                        from cafe in cafeGroup.DefaultIfEmpty()
                        select new EmployeesByCafeNameDto
                        {
                            EmployeeId = employee.Id,
                            Name = employee.Name,
                            EmailAddress = employee.EmailAddress,
                            Gender = employee.Gender,
                            PhoneNumber = employee.PhoneNumber,
                            DaysWorked = cafe == null ? 0 : (DateTime.Today - employee.StartedAt).Days,
                            CafeName = cafe?.Name
                        };

            var entities = query.ToList();
            return entities.OrderByDescending(e => e.DaysWorked);
        }
    }
}