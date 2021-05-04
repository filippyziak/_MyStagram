using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyStagram.Core.Logic.Requests.Query.Messenger;
using MyStagram.Core.Logic.Responses.Query.Messenger;
using MyStagram.Core.Services.Interfaces;
using MyStagram.Core.Services.Interfaces.ReadOnly;

namespace MyStagram.Core.Logic.Handlers.Query.Messenger
{
    public class CountUnreadMessagesQuery : IRequestHandler<CountUnreadMessagesRequest, CountUnreadMessagesResponse>
    {
        private readonly IReadOnlyMessenger messenger;
        public CountUnreadMessagesQuery(IReadOnlyMessenger messenger)
        {
            this.messenger = messenger;
        }
        public async Task<CountUnreadMessagesResponse> Handle(CountUnreadMessagesRequest request, CancellationToken cancellationToken)
        => new CountUnreadMessagesResponse { UnreadMessagesCount = await messenger.CountUnreadMessages(request.UserId) };
    }
}