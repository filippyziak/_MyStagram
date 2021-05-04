using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyStagram.Core.Extensions;
using MyStagram.Core.Logging;
using MyStagram.Core.Logic.Requests.Command.Main;
using MyStagram.Core.Logic.Requests.Query.Main;

namespace MyStagram.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class MainController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly INLogger logger;

        public MainController(IMediator mediator, INLogger logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }

        [HttpPost("post/create")]
        public async Task<IActionResult> CreatePost([FromForm] CreatePostRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{HttpContext.GetCurrentUserId()} created post #{response.Post?.Id}", response.Error);

            return response.IsSucceeded ? (IActionResult)Ok(response) : BadRequest(response);
        }

        [HttpDelete("post/delete")]
        public async Task<IActionResult> DeletePost([FromQuery] DeletePostRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{HttpContext.GetCurrentUserId()} deleted post #{request.PostId}", response.Error);

            return response.IsSucceeded ? (IActionResult)Ok(response) : BadRequest(response);
        }

        [HttpPut("post/update")]
        public async Task<IActionResult> UpdatePost([FromForm] UpdatePostRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{HttpContext.GetCurrentUserId()} updated post #{response.Post?.Id}", response.Error);

            return response.IsSucceeded ? (IActionResult)Ok(response) : BadRequest(response);
        }

        [HttpGet("post")]
        public async Task<IActionResult> GetPost([FromQuery] GetPostRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{HttpContext.GetCurrentUserId()} displayed post #{request.PostId}", response.Error);

            return response.IsSucceeded ? (IActionResult)Ok(response) : BadRequest(response);
        }

        [HttpGet("posts")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPosts([FromQuery] GetPostsRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{HttpContext.GetCurrentUserId()} displayed posts of #{request.UserId}", response.Error);

            return response.IsSucceeded ? (IActionResult)Ok(response) : BadRequest(response);
        }

        [HttpGet("fetch/posts")]
        public async Task<IActionResult> FetchPosts([FromQuery] FetchPostsRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{HttpContext.GetCurrentUserId()} displayed posts of following", response.Error);

            return response.IsSucceeded ? (IActionResult)Ok(response) : BadRequest(response);
        }

        [HttpPost("comment/create")]
        public async Task<IActionResult> CreateComment(CreateCommentRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{HttpContext.GetCurrentUserId()} created comment #{response.Comment?.Id} in post #{request.PostId}", response.Error);

            return response.IsSucceeded ? (IActionResult)Ok(response) : BadRequest(response);
        }

        [HttpDelete("comment/delete")]
        public async Task<IActionResult> DeleteComment([FromQuery] DeleteCommentRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{HttpContext.GetCurrentUserId()} deleted comment #{request.CommentId}", response.Error);

            return response.IsSucceeded ? (IActionResult)Ok(response) : BadRequest(response);
        }

        [HttpPut("like")]
        public async Task<IActionResult> LikePost(LikePostRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{HttpContext.GetCurrentUserId()} {(response.Result ? "LIKED" : "UNLIKED")} post #{request.PostId}", response.Error);

            return response.IsSucceeded ? (IActionResult)Ok(response) : BadRequest(response);
        }

    }
}