using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using MyStagram.Core.Exceptions;
using MyStagram.Core.Extensions;
using MyStagram.Core.Helpers;
using MyStagram.Core.Services.Interfaces;
using MyStagram.Core.Models.Domain.Auth;
using MyStagram.Core.Models.Domain.Main;
using MyStagram.Core.Models.Helpers.Pagination;
using MyStagram.Core.Logic.Requests.Query.Main;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MyStagram.Core.Models.Helpers.Result;
using MyStagram.Core.Data;
using MyStagram.Core.Services.Interfaces.ReadOnly;

namespace MyStagram.Core.Services
{
    public class MainService : IMainService
    {
        private readonly UserManager<User> userManager;
        private readonly IFilesService filesService;
        private readonly IDatabase database;
        private readonly string userId;
        private readonly IReadOnlyProfileService profileService;

        public MainService(UserManager<User> userManager, IFilesService filesService, IDatabase database,
        IHttpContextAccessor httpContextAccessor, IReadOnlyProfileService profileService)
        {
            this.filesService = filesService;
            this.database = database;
            this.userManager = userManager;
            this.userId = httpContextAccessor.HttpContext?.GetCurrentUserId();
            this.profileService = profileService;

        }

        #region Posts
        public async Task<Post> GetPost(string postId)
        => await database.PostRepository.Get(postId) ?? throw new EntityNotFoundException("Post doesn't exists");

        public async Task<PagedList<Post>> GetPosts(GetPostsRequest request)
        {
            IEnumerable<Post> posts;
            posts = (await database.PostRepository.GetWhere(p => p.UserId == request.UserId)).OrderByDescending(p => p.Created);

            return posts.ToPagedList<Post>(request.PageNumber, request.PageSize);
        }

        public async Task<Post> CreatePost(Post post, IFormFile photo)
        {
            var user = await userManager.FindByIdAsync(userId) ?? throw new EntityNotFoundException("Account does not exist", ErrorCodes.EntityNotFound);

            user.Posts.Add(post);

            if (!await database.Complete())
                return null;

            if (photo != null)
            {
                var uploaded = await filesService.UploadFile(photo, $"posts/{post.Id}", Path.GetExtension(photo.FileName));

                post.SetPhoto(uploaded?.FileUrl);
                database.FileRepository.AddFile(uploaded?.FileUrl, uploaded?.FilePath);

                return await database.Complete() ? post : null;
            }

            return post;
        }

        public async Task<PagedList<Post>> FetchPosts(FetchPostsRequest request)
        {
            var user = await profileService.GetCurrentUser();
            var following = user.Following;
            List<Post> posts = new List<Post>();

            foreach (var follower in following)
            {
                posts.AddRange(follower.Recipient.Posts);
            }

            return posts.OrderByDescending(p => p.Created).ToPagedList<Post>(request.PageNumber, request.PageSize);
        }

        public async Task<Post> UpdatePost(Post post)
        {
            var user = await userManager.FindByIdAsync(userId) ?? throw new EntityNotFoundException("Account does not exist", ErrorCodes.EntityNotFound);

            if (post.UserId != user.Id)
                throw new NoPermissionsException("You have no permission");

            database.PostRepository.Update(post);

            return await database.Complete() ? post : null;
        }
        public async Task<bool> DeletePost(string postId)
        {
            var user = await userManager.FindByIdAsync(userId) ?? throw new EntityNotFoundException("Account does not exist", ErrorCodes.EntityNotFound);
            var post = user.Posts.FirstOrDefault(p => p.Id == postId) ?? await database.PostRepository.Get(postId)
            ?? throw new EntityNotFoundException("Post not found");

            if (user.Id != post.UserId)
                throw new NoPermissionsException("You have no permission to delete this post");

            database.PostRepository.Delete(post);

            if (!await database.Complete())
                return false;

            if (post.PhotoUrl != null)
            {
                string filesPath = $"files/posts/{post.Id}";
                filesService.DeleteDirecetory(filesPath);
                await database.FileRepository.DeleteFileByPath(filesPath);

                await database.Complete();
            }

            return true;
        }
        #endregion

        #region Comments

        public async Task<Comment> CreateComment(string postId, string content)
        {
            var user = await userManager.FindByIdAsync(userId) ?? throw new EntityNotFoundException("Account does not exist", ErrorCodes.EntityNotFound);
            var post = await GetPost(postId);

            var comment = Comment.Create(content);

            user.Comments.Add(comment);
            post.Comments.Add(comment);

            return await database.Complete() ? comment : null;

        }

        public async Task<bool> DeleteComment(string commentId)
        {
            var user = await userManager.FindByIdAsync(userId) ?? throw new EntityNotFoundException("Account does not exist", ErrorCodes.EntityNotFound);
            var comment = user.Comments.FirstOrDefault(c => c.Id == commentId) ?? await database.CommentRepository.Get(commentId)
            ?? throw new EntityNotFoundException("Comment not found");

            if (user.Id != comment.UserId)
                throw new NoPermissionsException("You have no permission to delete this comment");

            database.CommentRepository.Delete(comment);

            return await database.Complete();
        }
        #endregion

        #region Likes

        public async Task<LikeResult> LikePost(string postId)
        {
            var user = await userManager.FindByIdAsync(userId) ?? throw new EntityNotFoundException("Account does not exist", ErrorCodes.EntityNotFound);
            var post = await GetPost(postId);

            var like = post.Likes.SingleOrDefault(l => l.UserId == user.Id);

            if (like != null)
            {
                post.Likes.Remove(like);
                return await database.Complete() ? new LikeResult() : null;
            }

            var newLike = Like.Create(user.Id, post.Id);

            user.Likes.Add(newLike);
            post.Likes.Add(newLike);

            return await database.Complete() ? new LikeResult(true, newLike) : null;
        }
        #endregion
    }
}