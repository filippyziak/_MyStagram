using System.Linq;
using System.Threading.Tasks;
using MyStagram.Core.Data.Models;
using MyStagram.Core.Data.Repositories;
using MyStagram.Core.Extensions;
using MyStagram.Core.Logic.Requests.Query.Social;
using MyStagram.Core.Models.Domain.Social;
using MyStagram.Core.Models.Helpers.Result;

namespace MyStagram.Infrastructure.Database.Repositories
{
    public class FollowerRepository : Repository<Follower>, IFollowerRepository
    {
        public FollowerRepository(DataContext context) : base(context)
        {
        }

        public async Task<GetFollowersResult> GetFollowers(GetFollowersRequest request)
        {
            IPagedList<Follower> followers;

            if (request.AreAccepted)
            {
                followers = string.IsNullOrEmpty(request.UserName)
              ? await context.Followers.Where(f => (f.RecipientId == request.UserId && f.RecipientAccepted))
                    .OrderByDescending(f => f.Created)
                    .ToPagedList<Follower>(request.PageNumber, request.PageSize)

              : await context.Followers.Where(f => (f.RecipientId == request.UserId && f.RecipientAccepted && f.Recipient.UserName.ToLower().Contains(request.UserName.ToLower())))
                  .OrderByDescending(f => f.Created)
                  .ToPagedList<Follower>(request.PageNumber, request.PageSize);
            }
            else
            {
                followers = await context.Followers.Where(f => (f.RecipientId == request.UserId))
                    .OrderByDescending(f => f.Created)
                   .ToPagedList<Follower>(request.PageNumber, request.PageSize);
            }

            var following = string.IsNullOrEmpty(request.UserName)
            ? await context.Followers.Where(f => (f.SenderId == request.UserId))
                .OrderByDescending(f => f.Created)
                .ToPagedList<Follower>(request.PageNumber, request.PageSize)
            : await context.Followers.Where(f => (f.SenderId == request.UserId) && f.Sender.UserName.ToLower().Contains(request.UserName.ToLower()))
                .OrderByDescending(f => f.Created)
                .ToPagedList<Follower>(request.PageNumber, request.PageSize);

            return new GetFollowersResult(followers, following);
        }
    }
}