using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyStagram.Core.Extensions;
using MyStagram.Core.Helpers;
using MyStagram.Core.Logging;
using MyStagram.Core.Logic.Requests.Command.Admin;
using MyStagram.Core.Logic.Requests.Query.Admin;

namespace MyStagram.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = Constants.AdminPolicy)]
    public class AdminController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly INLogger logger;

        public AdminController(IMediator mediator, INLogger logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }

        [HttpGet("logs")]
        public async Task<IActionResult> GetLogs([FromQuery] GetLogsRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"Admin #{HttpContext.GetCurrentUserId()} filtered logs", response.Error);

            return response.IsSucceeded ? (IActionResult)Ok(response) : BadRequest(response);
        }

        [HttpPatch("user/block")]
        public async Task<IActionResult> BlockUser(BlockUserRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"Admin #{HttpContext.GetCurrentUserId()} blocked #{request.UserId} user", response.Error);

            return response.IsSucceeded ? (IActionResult)Ok(response) : BadRequest(response);
        }

        [HttpPatch("user/admit")]
        public async Task<IActionResult> AdmitRole(AdmitRoleRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"Admin #{HttpContext.GetCurrentUserId()} admitted #{request.RoleId} role to #{request.UserId} user", response.Error);

            return response.IsSucceeded ? (IActionResult)Ok(response) : BadRequest(response);
        }

        [HttpPatch("user/revoke")]
        public async Task<IActionResult> RevokeRole(RevokeRoleRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"Admin #{HttpContext.GetCurrentUserId()} revoked #{request.RoleId} role of #{request.UserId} user", response.Error);

            return response.IsSucceeded ? (IActionResult)Ok(response) : BadRequest(response);
        }

        [HttpDelete("user/delete")]
        [Authorize(Policy = Constants.HeadAdminPolicy)]
        public async Task<IActionResult> DeleteUser([FromQuery] DeleteUserRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"HeadAdmin #{HttpContext.GetCurrentUserId()} deleted #{request.UserId} user", response.Error);

            return response.IsSucceeded ? (IActionResult)Ok(response) : BadRequest(response);
        }
    }
}