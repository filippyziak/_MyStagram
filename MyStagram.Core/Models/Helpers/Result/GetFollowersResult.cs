using MyStagram.Core.Models.Helpers.Pagination;
using MyStagram.Core.Models.Domain.Social;

namespace MyStagram.Core.Models.Helpers.Result
{
    public class GetFollowersResult
    {

        public PagedList<Follower> Followers { get; }
        public PagedList<Follower> Following { get; }
        
        public GetFollowersResult(PagedList<Follower> followers, PagedList<Follower> following)
        {
            Followers = followers;
            Following = following;
        }
    }
}