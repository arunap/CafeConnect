using System.Reflection;
using CafeConnect.Application.Features.Employee.Commands;
using CafeConnect.Application.Features.Employee.Dtos;
using CafeConnect.Application.Features.Employee.Queries;
using log4net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CafeConnect.Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;
        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);

        public EmployeeController(IMediator mediator) => _mediator = mediator;

        // GET: api/Employee/{id}
        [HttpGet("Employee/{id}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployee(string id)
        {
            var query = new GetEmployeeQuery { Id = id };
            var result = await _mediator.Send(query);

            return result == null ? NotFound() : Ok(result);
        }

        // GET: api/Employees
        [HttpGet("Employees")]
        public async Task<ActionResult<IEnumerable<EmployeesByCafeNameDto>>> GetEmployees(string? cafe)
        {
            var query = new GetAllEmployeesQuery { CafeName = cafe };
            var result = await _mediator.Send(query);

            return result == null ? NotFound() : Ok(result);
        }

        // POST: api/Employee
        [HttpPost("Employee")]
        public async Task<ActionResult<EmployeeDto>> PostEmployee(CreateEmployeeCommand command)
        {
            var insertedId = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetEmployee), new { id = insertedId }, command);
        }

        // PUT: api/Employee/{id}
        [HttpPut("Employee/{id}")]
        public async Task<IActionResult> PutEmployee(string id, UpdateEmployeeCommand command)
        {
            try
            {
                if (id != command.EmployeeId) return BadRequest();

                await _mediator.Send(command);

                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                _logger.Warn("Concurrency exception occurred. Data may have been modified by another user.");

                // Inform the user to reload data
                return Conflict("The data has been updated by another user. Please reload the data.");
            }
        }


        // DELETE: api/Employee/{id}
        [HttpDelete("Employee/{id}")]
        public async Task<IActionResult> DeleteEmployee(string id)
        {
            var command = new DeleteEmployeeCommand(id);
            await _mediator.Send(command);

            return NoContent();
        }
    }
}