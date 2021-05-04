using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyStagram.Core.Extensions;
using MyStagram.Core.Logging;
using MyStagram.Core.Logic.Requests.Command.Connection;

namespace MyStagram.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConnectionController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly INLogger logger;

        public ConnectionController(IMediator mediator, INLogger logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }

        [HttpPost("start")]
        public async Task<IActionResult> StartConnection(StartConnectionRequest request)
        {
            var response = await mediator.Send(request);
            
            logger.LogResponse($"User #{HttpContext.GetCurrentUserId()} started SignalR connection #{request.ConnectionId}", response.Error);

            return response.IsSucceeded ? (IActionResult)Ok(response) : BadRequest(response);
        }

        [HttpDelete("close")]
        public async Task<IActionResult> CloseConnection([FromQuery] CloseConnectionRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{HttpContext.GetCurrentUserId()} closed SignalR connection #{request.ConnectionId}", response.Error);

            return response.IsSucceeded ? (IActionResult)Ok(response) : BadRequest(response);
        }
    }
}
