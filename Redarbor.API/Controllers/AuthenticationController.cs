using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Redarbor.Application.Authentication.Queries;

namespace Redarbor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator Mediator;
        private readonly ILogger<AuthenticationController> Logger;

        public AuthenticationController(IMediator mediator, ILogger<AuthenticationController> logger)
        {
            Logger = logger;
            Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] GenerateTokenQuery query)
        {
            string result = await Mediator.Send(query);
            Logger.LogInformation("Token generated");
            return Ok(result);
        }
    }
}