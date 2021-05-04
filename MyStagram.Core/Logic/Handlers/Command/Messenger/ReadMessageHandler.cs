using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyStagram.Core.Helpers;
using MyStagram.Core.Logic.Requests.Command.Messenger;
using MyStagram.Core.Logic.Responses.Command.Messenger;
using MyStagram.Core.Models.Helpers.Error;
using MyStagram.Core.Services.Interfaces;

namespace MyStagram.Core.Logic.Handlers.Command.Messenger
{
    public class ReadMessageHandler : IRequestHandler<ReadMessageRequest, ReadMessageResponse>
    {
        private readonly IMessenger messenger;
        public ReadMessageHandler(IMessenger messenger)
        {
            this.messenger = messenger;
        }

        public async Task<ReadMessageResponse> Handle(ReadMessageRequest request, CancellationToken cancellationToken)
        => await messenger.ReadMessage(request.MessageId) ? new ReadMessageResponse()
        : new ReadMessageResponse(Error.Build
        (
            ErrorCodes.CrudActionFailed,
            "Cannot read message"
        ));
    }
}