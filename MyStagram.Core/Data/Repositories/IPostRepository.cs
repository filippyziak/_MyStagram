using System.Threading.Tasks;
using MyStagram.Core.Data.Models;
using MyStagram.Core.Logic.Requests.Query.Main;
using MyStagram.Core.Models.Domain.Main;
using MyStagram.Core.Models.Helpers.Pagination;

namespace MyStagram.Core.Data.Repositories
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<IPagedList<Post>> GetPosts(GetPostsRequest request);
        
    }
}