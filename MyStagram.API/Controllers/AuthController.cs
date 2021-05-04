using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyStagram.Core.Enums;
using MyStagram.Core.Extensions;
using MyStagram.Core.Logging;
using MyStagram.Core.Logic.Requests.Command.Auth;
using MyStagram.Core.Logic.Requests.Query.Auth;

namespace MyStagram.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly INLogger logger;

        public AuthController(IMediator mediator, INLogger logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User {request.Email} #{response.User.Id} logged in", response.Error);

            return response.IsSucceeded ? (IActionResult)Ok(response) : BadRequest(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User {request.Email} registered", response.Error);

            return response.IsSucceeded ? (IActionResult)Ok(response) : BadRequest(response);
        }

        [HttpGet("account/confirm")]
        public async Task<IActionResult> ConfirmAccount([FromQuery] ConfirmAccountRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{request.UserId} confirmed account", response.Error);

            return response.IsSucceeded ? (IActionResult)Ok(response) : BadRequest(response);
        }

        [HttpGet("register/validate")]
        public async Task<IActionResult> GetRegisterValidation([FromQuery] GetRegisterValidationRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User {request.Content} verified {(request.RegisterValidation == RegisterValidation.Email ? "EMAIL" : "USERNAME")} availability", response.Error);

            return response.IsSucceeded ? (IActionResult)Ok(response) : BadRequest(response);
        }
        [HttpPost("account/resetPassword/send")]
        public async Task<IActionResult> SendResetPassword(SendResetPasswordRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User {request.Email} sent reset password email", response.Error);

            return response.IsSucceeded ? (IActionResult)Ok(response) : BadRequest(response);
        }

        [HttpPatch("account/resetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{request.UserId} resetted password", response.Error);;

            return response.IsSucceeded ? (IActionResult)Ok(response) : BadRequest(response);
        }

        [HttpGet("account/resetPassword/confirm")]
        public async Task<IActionResult> ConfirmResetPassword([FromQuery] ConfirmResetPasswordRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{request.UserId} verified reset password link", response.Error);

            return response.IsSucceeded ? (IActionResult)Ok(response) : BadRequest(response);
        }

    }
}
