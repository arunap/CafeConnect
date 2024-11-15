using System.Reflection;
using CafeConnect.Application.Features.Cafe.Commands;
using CafeConnect.Application.Features.Cafe.Dtos;
using CafeConnect.Application.Features.Cafe.Queries;
using log4net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CafeConnect.Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class CafeController : ControllerBase
    {
        private readonly IMediator _mediator;
        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType);
        public CafeController(IMediator mediator) => _mediator = mediator;

        // GET: api/Cafe/{id}
        [HttpGet("Cafe/{id}")]
        public async Task<ActionResult<CafeDto>> GetCafe(Guid id)
        {
            var query = new GetCafeQuery() { CafeId = id };
            var result = await _mediator.Send(query);

            return result == null ? NotFound() : Ok(result);
        }

        // GET: api/Cafe
        [HttpGet("Cafes")]
        public async Task<ActionResult<IEnumerable<CafesByLocationDto>>> GetCafes([FromQuery] string? location)
        {
            var query = new GetAllCafesQuery { Location = location };
            var result = await _mediator.Send(query);

            return result == null ? NotFound() : Ok(result);
        }

        // POST: api/Cafe
        [HttpPost("Cafe")]
        public async Task<ActionResult<CafeDto>> PostCafe([FromForm] CreateCafeCommand command)
        {
            var insertedId = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetCafe), new { id = insertedId }, command);
        }

        // PUT: api/Cafe/{id}
        [HttpPut("Cafe/{id}")]
        public async Task<IActionResult> PutCafe(Guid id, [FromForm] UpdateCafeCommand command)
        {
            try
            {
                if (id != command.Id) return BadRequest();

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


        // DELETE: api/Cafe/{id}
        [HttpDelete("Cafe/{id}")]
        public async Task<IActionResult> DeleteCafe(Guid id)
        {
            var command = new DeleteCafeCommand { Id = id };
            await _mediator.Send(command);

            return NoContent();
        }
    }
}