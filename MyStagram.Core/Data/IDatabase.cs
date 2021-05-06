using System;
using System.Threading.Tasks;
using MyStagram.Core.Data.Repositories;
using MyStagram.Core.Models.Domain.Auth;
using MyStagram.Core.Models.Domain.Connection;
using MyStagram.Core.Models.Domain.Main;
using MyStagram.Core.Models.Domain.Social;

namespace MyStagram.Core.Data
{
    public interface IDatabase : IDisposable
    {
        IUserRepository UserRepository { get; }
        IRepository<Role> RoleRepository { get; }
        IRepository<UserRole> UserRoleRepository { get; }
        IPostRepository PostRepository { get; }
        IRepository<Comment> CommentRepository { get; }
        IRepository<Like> LikeRepository { get; }
        IFollowerRepository FollowerRepository { get; }
        IRepository<Connection> ConnectionRepository { get; }
        IMessageRepository MessageRepository { get; }
        IRepository<Story> StoryRepository { get; }
        IRepository<UserStory> UserStoryRepository { get; }
        IFileRepository FileRepository { get; }


        Task<bool> Complete();

        bool HasChanges();
    }
}