using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyStagram.Core.Extensions;
using MyStagram.Core.Logging;
using MyStagram.Core.Logic.Requests.Command.Messenger;
using MyStagram.Core.Logic.Requests.Query.Messenger;

namespace MyStagram.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessengerController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly INLogger logger;

        public MessengerController(IMediator mediator, INLogger logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }

        [HttpPost("message/send")]
        public async Task<IActionResult> MessageSend(MessageSendRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{HttpContext.GetCurrentUserId()} sent message to friend #{request.RecipientId}", response.Error);

            return response.IsSucceeded ? (IActionResult)Ok(response) : BadRequest(response);
        }

        [HttpGet("messages")]
        public async Task<IActionResult> GetMessagesThread([FromQuery] GetMessagesThreadRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{HttpContext.GetCurrentUserId()} opened chat with user #{request.RecipientId}", response.Error);

            return response.IsSucceeded ? (IActionResult)Ok(response) : BadRequest(response);
        }

        [HttpGet("conversations")]
        public async Task<IActionResult> GetConversations([FromQuery] GetConversationsRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{HttpContext.GetCurrentUserId()} filtered their conversations", response.Error);

            return response.IsSucceeded ? (IActionResult)Ok(response) : BadRequest(response);
        }

        [HttpGet("conversations/count")]
        public async Task<IActionResult> CountUnreadConversations([FromQuery] CountUnreadConversationsRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{HttpContext.GetCurrentUserId()} has {response?.UnreadConversationsCount} unseen conversations", response.Error);

            return response.IsSucceeded ? (IActionResult)Ok(response) : BadRequest(response);
        }

        [HttpGet("messages/count")]
        public async Task<IActionResult> CountUnreadMessages([FromQuery] CountUnreadMessagesRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{HttpContext.GetCurrentUserId()} has {response?.UnreadMessagesCount} unread messages", response.Error);

            return response.IsSucceeded ? (IActionResult)Ok(response) : BadRequest(response);
        }

        [HttpPatch("message/read")]
        public async Task<IActionResult> ReadMessage(ReadMessageRequest request)
        {
            var response = await mediator.Send(request);

            logger.LogResponse($"User #{HttpContext.GetCurrentUserId()} has read #{request.MessageId} message", response.Error);

            return response.IsSucceeded ? (IActionResult)Ok(response) : BadRequest(response);
        }
    }
}