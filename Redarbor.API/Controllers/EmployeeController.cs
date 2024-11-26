using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Redarbor.Application.Employees.Commands;
using Redarbor.Application.Employees.Queries;

namespace Redarbor.API.Controllers
{
    [ApiController]
    [Route("api/redarbor")]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> Logger;
        private readonly IMediator Mediator;

        public EmployeeController(IMediator mediator, ILogger<EmployeeController> logger)
        {
            Logger = logger;
            Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddEmployeeCommand request)
        {
            int result = await Mediator.Send(request);
            Logger.LogInformation("Employee created");
            return CreatedAtAction(nameof(GetById), new { id = result }, request);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await Mediator.Send(new GetAllEmployeeQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await Mediator.Send(new GetByIdEmployeeQuery() { Id = id });

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateEmployeeCommand command)
        {
            command.Id = id;
            await Mediator.Send(command);
            Logger.LogInformation("Employee updated");
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteEmployeeCommand { Id = id });
            Logger.LogInformation("Employee deleted");
            return Ok();
        }
    }
}