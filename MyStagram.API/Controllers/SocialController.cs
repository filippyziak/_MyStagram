using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyStagram.Core.Extensions;
using MyStagram.Core.Logging;
using MyStagram.Core.Logic.Requests.Command.Social;
using MyStagram.Core.Logic.Requests.Query.Social;

namespace MyStagram.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SocialController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly INLogger logger;

        public SocialController(IMediator mediator, INLogger logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }

        [HttpPost("follow")]
        public async Task<IActionResult> FollowUser(FollowUserRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{HttpContext.GetCurrentUserId()} followed user #{request.RecipientId}", response.Error);

            return response.IsSucceeded ? (IActionResult)Ok(response) : BadRequest(response);
        }

        [HttpDelete("unfollow")]
        public async Task<IActionResult> UnFollowUser([FromQuery] UnFollowUserRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{HttpContext.GetCurrentUserId()} unfollowed user #{request.RecipientId}", response.Error);

            return response.IsSucceeded ? (IActionResult)Ok(response) : BadRequest(response);
        }

        [HttpPut("follow/accept")]
        public async Task<IActionResult> AcceptFollower(AcceptFollowerRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{HttpContext.GetCurrentUserId()} {(request.Accepted ? "Accepted" : "Declined")} follow of user #{request.RecipientId}", response.Error);

            return response.IsSucceeded ? (IActionResult)Ok(response) : BadRequest(response);
        }

        [HttpGet("followers")]
        public async Task<IActionResult> GetFollowers([FromQuery] GetFollowersRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{HttpContext.GetCurrentUserId()} fetched followers", response.Error);

            return response.IsSucceeded ? (IActionResult)Ok(response) : BadRequest(response);
        }

        [HttpGet("followers/unwatched")]
        public async Task<IActionResult> CountUnwatchedFollowers([FromQuery] CountUnwatchedFollowersRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{HttpContext.GetCurrentUserId()} has {response?.UnwatchedFollowsCount} unseen follows", response.Error);

            return response.IsSucceeded ? (IActionResult)Ok(response) : BadRequest(response);
        }

        [HttpPatch("follower/watch")]
        public async Task<IActionResult> WatchFollower(WatchFollowerRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{HttpContext.GetCurrentUserId()} has seen #{request?.SenderId} follow", response.Error);

            return response.IsSucceeded ? (IActionResult)Ok(response) : BadRequest(response);
        }
    }
}