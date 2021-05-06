using MyStagram.Core.Models.Helpers.Pagination;
using MyStagram.Core.Models.Domain.Social;
using MyStagram.Core.Data.Models;

namespace MyStagram.Core.Models.Helpers.Result
{
    public class GetFollowersResult
    {

        public IPagedList<Follower> Followers { get; set; }
        public IPagedList<Follower> Following { get; }

        public GetFollowersResult(IPagedList<Follower> followers, IPagedList<Follower> following)
        {
            Followers = followers;
            Following = following;
        }
    }
}