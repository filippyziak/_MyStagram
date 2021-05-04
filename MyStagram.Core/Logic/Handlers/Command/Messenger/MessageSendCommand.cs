using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MyStagram.Core.Helpers;
using MyStagram.Core.Logic.Requests.Command.Messenger;
using MyStagram.Core.Logic.Responses.Command.Messenger;
using MyStagram.Core.Models.Helpers.Error;
using MyStagram.Core.Services.Interfaces;
using MyStagram.Core.Services.SignalR;
using MyStagram.Core.Models.Dtos.Social;

namespace MyStagram.Core.Logic.Handlers.Command.Messenger
{
    public class MessageSendCommand : IRequestHandler<MessageSendRequest, MessageSendResponse>
    {
        private readonly IMapper mapper;
        private readonly IMessenger messenger;
        private readonly IHubManager hubManager;
        public MessageSendCommand(IMapper mapper, IMessenger messenger, IHubManager hubManager)
        {
            this.mapper = mapper;
            this.messenger = messenger;
            this.hubManager = hubManager;

        }
        public async Task<MessageSendResponse> Handle(MessageSendRequest request, CancellationToken cancellationToken)
        {
            var message = await this.messenger.Send(request.RecipientId, request.Content);

            if (message != null)
            {
                await hubManager.Invoke(SignalRActions.ON_MESSAGE_SEND, request.RecipientId, mapper.Map<MessageDto>(message));

                return new MessageSendResponse { Message = mapper.Map<MessageDto>(message) };
            }

            return new MessageSendResponse(Error.Build
            (
                ErrorCodes.CrudActionFailed,
                "Send message failed"
            ));
        }
    }
}