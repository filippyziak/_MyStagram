using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using MyStagram.Core.Extensions;
using MyStagram.Core.Logic.Requests.Query.Messenger;
using MyStagram.Core.Logic.Responses.Query.Messenger;
using MyStagram.Core.Services.Interfaces;
using MyStagram.Core.Services.Interfaces.ReadOnly;

namespace MyStagram.Core.Logic.Handlers.Query.Messenger
{
    public class GetConversationsQuery : IRequestHandler<GetConversationsRequest, GetConversationsResponse>
    {
        private readonly IMapper mapper;
        private readonly IReadOnlyMessenger messenger;
        private readonly IHttpContextAccessor httpContextAccessor;
        public GetConversationsQuery(IMapper mapper, IReadOnlyMessenger messenger, IHttpContextAccessor httpContextAccessor)
        {
            this.mapper = mapper;
            this.messenger = messenger;
            this.httpContextAccessor = httpContextAccessor;

        }
        public async Task<GetConversationsResponse> Handle(GetConversationsRequest request, CancellationToken cancellationToken)
        {
            var conversations = await messenger.GetConverstaions(request);

            httpContextAccessor.HttpContext.Response.AddPagination(conversations.CurrentPage, conversations.PageSize, conversations.TotalCount, conversations.TotalPages);

            return new GetConversationsResponse { Conversations = conversations };
        }
    }
}