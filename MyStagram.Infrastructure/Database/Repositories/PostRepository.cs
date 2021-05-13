using System.Linq;
using System.Threading.Tasks;
using MyStagram.Core.Data.Models;
using MyStagram.Core.Data.Repositories;
using MyStagram.Core.Extensions;
using MyStagram.Core.Logic.Requests.Query.Main;
using MyStagram.Core.Models.Domain.Main;

namespace MyStagram.Infrastructure.Database.Repositories
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(DataContext context) : base(context)
        {
        }

        public async Task<IPagedList<Post>> GetPosts(GetPostsRequest request)
        => await context.Posts.Where(p => p.UserId == request.UserId).OrderByDescending(p => p.Created).ToPagedList(request.PageNumber, request.PageSize);

    }
}