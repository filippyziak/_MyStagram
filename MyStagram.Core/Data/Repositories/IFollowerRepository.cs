using System.Threading.Tasks;
using MyStagram.Core.Data.Models;
using MyStagram.Core.Logic.Requests.Query.Social;
using MyStagram.Core.Models.Domain.Social;
using MyStagram.Core.Models.Helpers.Result;

namespace MyStagram.Core.Data.Repositories
{
    public interface IFollowerRepository : IRepository<Follower>
    {
        Task<GetFollowersResult> GetFollowers(GetFollowersRequest request);
    }
}