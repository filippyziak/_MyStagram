using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using MyStagram.Core.Extensions;
using MyStagram.Core.Logic.Requests.Query.Messenger;
using MyStagram.Core.Logic.Responses.Query.Messenger;
using MyStagram.Core.Models.Dtos.Messenger;
using MyStagram.Core.Models.Dtos.Social;
using MyStagram.Core.Services.Interfaces.ReadOnly;

namespace MyStagram.Core.Logic.Handlers.Query.Messenger
{
    public class GetMessagesThreadQuery : IRequestHandler<GetMessagesThreadRequest, GetMessagesThreadResponse>
    {
        private readonly IReadOnlyMessenger messenger;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IReadOnlyProfileService profileService;
        public GetMessagesThreadQuery(IReadOnlyMessenger messenger, IMapper mapper, IHttpContextAccessor httpContextAccessor,
                                        IReadOnlyProfileService profileService)
        {
            this.messenger = messenger;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
            this.profileService = profileService;

        }
        public async Task<GetMessagesThreadResponse> Handle(GetMessagesThreadRequest request, CancellationToken cancellationToken)
        {
            var messages = await messenger.GetMessagesThread(request);

            httpContextAccessor.HttpContext.Response.AddPagination(messages.CurrentPage, messages.PageSize, messages.TotalCount, messages.TotalPages);

            return new GetMessagesThreadResponse
            {
                Messages = mapper.Map<List<MessageDto>>(messages),
                Recipient = mapper.Map<RecipientDto>(await profileService.GetUser(request.RecipientId))
            };
        }
    }
}