using System.Threading.Tasks;
using MyStagram.Core.Data.Models;
using MyStagram.Core.Logic.Requests.Query.Main;
using MyStagram.Core.Models.Domain.Main;
using MyStagram.Core.Models.Helpers.Pagination;

namespace MyStagram.Core.Services.Interfaces.ReadOnly
{
    public interface IReadOnlyMainService
    {
        Task<Post> GetPost(string postId);
        Task<IPagedList<Post>> GetPosts(GetPostsRequest request);
        Task<PagedList<Post>> FetchPosts(FetchPostsRequest request);
    }
}