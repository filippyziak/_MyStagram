using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyStagram.Core.Extensions;
using MyStagram.Core.Logging;
using MyStagram.Core.Logic.Requests.Command.Story;
using MyStagram.Core.Logic.Requests.Query.Story;

namespace MyStagram.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StoryController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly INLogger logger;

        public StoryController(IMediator mediator, INLogger logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateStory([FromForm] CreateStoryRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{HttpContext.GetCurrentUserId()} created story #{response.Story?.Id}", response.Error);

            return response.IsSucceeded ? (IActionResult)Ok(response) : BadRequest(response);
        }

        [HttpGet("stories")]
        public async Task<IActionResult> GetStories([FromQuery] GetStoriesRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{HttpContext.GetCurrentUserId()} fetched stories", response.Error);

            return response.IsSucceeded ? (IActionResult)Ok(response) : BadRequest(response);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteStory([FromQuery] DeleteStoryRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{HttpContext.GetCurrentUserId()} deleted story #{request.StoryId}", response.Error);

            return response.IsSucceeded ? (IActionResult)Ok(response) : BadRequest(response);
        }

        [HttpGet("user/stories")]
        public async Task<IActionResult> GetThisUserStories([FromQuery] GetThisUserStoriesRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{HttpContext.GetCurrentUserId()} fetched #{request.UserId} stories", response.Error);

            return response.IsSucceeded ? (IActionResult)Ok(response) : BadRequest(response);
        }

        [HttpPost("watch")]
        public async Task<IActionResult> WatchStory(WatchStoryRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{HttpContext.GetCurrentUserId()} watched story #{request.StoryId}", response.Error);

            return response.IsSucceeded ? (IActionResult)Ok(response) : BadRequest(response);
        }
    }
}