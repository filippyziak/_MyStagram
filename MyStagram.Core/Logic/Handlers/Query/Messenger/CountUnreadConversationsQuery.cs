using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyStagram.Core.Logic.Requests.Query.Messenger;
using MyStagram.Core.Logic.Responses.Query.Messenger;
using MyStagram.Core.Services.Interfaces;
using MyStagram.Core.Services.Interfaces.ReadOnly;

namespace MyStagram.Core.Logic.Handlers.Query.Messenger
{
    public class CountUnreadConversationsQuery : IRequestHandler<CountUnreadConversationsRequest, CountUnreadConversationsResponse>
    {
        private readonly IReadOnlyMessenger messenger;
        public CountUnreadConversationsQuery(IReadOnlyMessenger messenger)
        {
            this.messenger = messenger;
        }
        public async Task<CountUnreadConversationsResponse> Handle(CountUnreadConversationsRequest request, CancellationToken cancellationToken)
        => new CountUnreadConversationsResponse { UnreadConversationsCount = await messenger.CountUnreadConversations() };
    }
}