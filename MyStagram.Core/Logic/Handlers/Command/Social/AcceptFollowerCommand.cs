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
    public class AcceptFollowerCommand : IRequestHandler<AcceptFollowerRequest, AcceptFollowerResponse>
    {
        private readonly IFollowersService followersService;
        private readonly IHubManager hubManager;
        public AcceptFollowerCommand(IFollowersService followersService, IHubManager hubManager)
        {
            this.followersService = followersService;
            this.hubManager = hubManager;

        }
        public async Task<AcceptFollowerResponse> Handle(AcceptFollowerRequest request, CancellationToken cancellationToken)
        {
            if (await this.followersService.Accept(request.SenderId, request.RecipientId, request.Accepted))
                return new AcceptFollowerResponse();

            return new AcceptFollowerResponse(Error.Build
            (
                ErrorCodes.CrudActionFailed,
                "You can't response this request"
            ));
        }
    }
}