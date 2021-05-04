using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyStagram.Core.Helpers;
using MyStagram.Core.Logic.Requests.Command.Social;
using MyStagram.Core.Logic.Responses.Command.Social;
using MyStagram.Core.Models.Helpers.Error;
using MyStagram.Core.Services.Interfaces;
using MyStagram.Core.Services.SignalR;

namespace MyStagram.Core.Logic.Handlers.Command.Social
{
    public class UnFollowUserCommand : IRequestHandler<UnFollowUserRequest, UnFollowUserResponse>
    {
        private readonly IFollowersService followersService;
        private readonly IHubManager hubManager;
        private readonly IProfileService profileService;

        public UnFollowUserCommand(IFollowersService followersService, IHubManager hubManager, IProfileService profileService)
        {
            this.followersService = followersService;
            this.hubManager = hubManager;
            this.profileService = profileService;

        }
        public async Task<UnFollowUserResponse> Handle(UnFollowUserRequest request, CancellationToken cancellationToken)
        {
            if (await followersService.UnFollowUser(request.RecipientId))
            {
                var sender = await profileService.GetCurrentUser();
                await hubManager.Invoke(SignalRActions.ON_UNFOLLOW_USER, request.RecipientId, sender.Id);
                return new UnFollowUserResponse();
            }

            return new UnFollowUserResponse(Error.Build
            (
                ErrorCodes.CrudActionFailed,
                "You can't unfollow this user"
            ));
        }
    }
}