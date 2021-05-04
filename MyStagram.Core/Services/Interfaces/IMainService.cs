using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MyStagram.Core.Models.Helpers.Result;
using MyStagram.Core.Models.Domain.Main;
using MyStagram.Core.Services.Interfaces.ReadOnly;

namespace MyStagram.Core.Services.Interfaces
{
    public interface IMainService : IReadOnlyMainService
    {
        Task<Post> CreatePost(Post post, IFormFile photo);
        Task<Post> UpdatePost(Post post);
        Task<bool> DeletePost(string postId);

        Task<Comment> CreateComment(string postId, string content);
        Task<bool> DeleteComment(string commentId);

        Task<LikeResult> LikePost(string postId);
    }
}