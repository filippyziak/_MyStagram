using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyStagram.Core.Helpers;
using MyStagram.Core.Logic.Requests.Command.Social;
using MyStagram.Core.Logic.Responses.Command.Social;
using MyStagram.Core.Models.Helpers.Error;
using MyStagram.Core.Services.Interfaces;

namespace MyStagram.Core.Logic.Handlers.Command.Social
{
    public class WatchFollowerHandler : IRequestHandler<WatchFollowerRequest, WatchFollowerResponse>
    {
        private readonly IFollowersService followersService;
        public WatchFollowerHandler(IFollowersService followersService)
        {
            this.followersService = followersService;
        }
        public async Task<WatchFollowerResponse> Handle(WatchFollowerRequest request, CancellationToken cancellationToken)
        => await followersService.WatchFollower(request.SenderId) ? new WatchFollowerResponse()
        : new WatchFollowerResponse(Error.Build
        (
            ErrorCodes.CrudActionFailed,
            "Cannot watch follower"
        ));
    }
}