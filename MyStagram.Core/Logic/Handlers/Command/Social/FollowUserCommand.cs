using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MyStagram.Core.Helpers;
using MyStagram.Core.Logic.Requests.Command.Social;
using MyStagram.Core.Logic.Responses.Command.Social;
using MyStagram.Core.Models.Helpers.Error;
using MyStagram.Core.Services.Interfaces;
using MyStagram.Core.Services.SignalR;
using MyStagram.Core.Models.Dtos.Social;

namespace MyStagram.Core.Logic.Handlers.Command.Social
{
    public class FollowUserCommand : IRequestHandler<FollowUserRequest, FollowUserResponse>
    {
        private readonly IFollowersService followersService;
        private readonly IMapper mapper;
        private readonly IHubManager hubManager;

        public FollowUserCommand(IFollowersService followersService, IMapper mapper, IHubManager hubManager)
        {
            this.followersService = followersService;
            this.mapper = mapper;
            this.hubManager = hubManager;

        }
        public async Task<FollowUserResponse> Handle(FollowUserRequest request, CancellationToken cancellationToken)
        {
            var followed = await followersService.FollowUser(request.RecipientId);

            if (followed != null)
            {
                await hubManager.Invoke(SignalRActions.ON_FOLLOW_USER, request.RecipientId, mapper.Map<FollowerDto>(followed));
                return new FollowUserResponse { Follower = mapper.Map<FollowerDto>(followed) };
            }

            return new FollowUserResponse(Error.Build
            (
                ErrorCodes.CrudActionFailed,
                "Follow Error"
            ));
        }
    }
}