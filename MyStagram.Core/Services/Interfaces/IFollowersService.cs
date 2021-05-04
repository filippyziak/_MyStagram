using System.Threading.Tasks;
using MyStagram.Core.Models.Domain.Social;
using MyStagram.Core.Services.Interfaces.ReadOnly;

namespace MyStagram.Core.Services.Interfaces
{
    public interface IFollowersService : IReadOnlyFollowersService
    {
        Task<Follower> FollowUser(string recipientId);
        Task<bool> UnFollowUser(string recipientId);
        Task<bool> Accept(string senderId, string recipientId, bool accepted);
        Task<bool> WatchFollower(string senderId);
    }
}