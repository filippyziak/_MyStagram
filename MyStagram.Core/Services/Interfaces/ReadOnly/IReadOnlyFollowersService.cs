using System.Threading.Tasks;
using MyStagram.Core.Logic.Requests.Query.Social;
using MyStagram.Core.Models.Domain.Social;
using MyStagram.Core.Models.Helpers.Result;

namespace MyStagram.Core.Services.Interfaces.ReadOnly
{
    public interface IReadOnlyFollowersService
    {
        Task<GetFollowersResult> GetFollowers(GetFollowersRequest request);
        Task<Follower> GetFollower(string senderId, string recipientId);
        Task<int> CountUnwatchedFollows();
    }
}