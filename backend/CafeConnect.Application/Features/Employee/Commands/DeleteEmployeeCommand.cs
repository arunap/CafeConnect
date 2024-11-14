using CafeConnect.Domain.Repositories;
using MediatR;

namespace CafeConnect.Application.Features.Employee.Commands
{
    public class DeleteEmployeeCommand : IRequest
    {
        public DeleteEmployeeCommand(string id) => this.Id = id;

        public string Id { get; set; }
    }

    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public DeleteEmployeeCommandHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var item = new Domain.Entities.Employee { Id = request.Id };
            await _employeeRepository.DeleteAsync(item);
        }
    }
}