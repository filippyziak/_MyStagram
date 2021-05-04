using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyStagram.Core.Logic.Requests.Query.Social;
using MyStagram.Core.Logic.Responses.Query.Social;
using MyStagram.Core.Services.Interfaces;
using MyStagram.Core.Services.Interfaces.ReadOnly;

namespace MyStagram.Core.Logic.Handlers.Query.Social
{
    public class CountUnwatchedFollowersQuery : IRequestHandler<CountUnwatchedFollowersRequest, CountUnwatchedFollowersResponse>
    {
        private readonly IReadOnlyFollowersService followersService;
        public CountUnwatchedFollowersQuery(IReadOnlyFollowersService followersService)
        {
            this.followersService = followersService;

        }
        public async Task<CountUnwatchedFollowersResponse> Handle(CountUnwatchedFollowersRequest request, CancellationToken cancellationToken)
            => new CountUnwatchedFollowersResponse { UnwatchedFollowsCount = await followersService.CountUnwatchedFollows() };


    }
}