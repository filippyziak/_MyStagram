using System.Threading.Tasks;
using MyStagram.Infrastructure.Database.Repositories;
using MyStagram.Core.Models.Domain.Auth;
using MyStagram.Core.Models.Domain.Connection;
using MyStagram.Core.Models.Domain.Main;
using MyStagram.Core.Models.Domain.Social;
using MyStagram.Core.Data;
using MyStagram.Core.Data.Repositories;

namespace MyStagram.Infrastructure.Database
{
    public class Database : IDatabase
    {
        private readonly DataContext context;
        public Database(DataContext context)
        {
            this.context = context;

        }
        #region repos
        private IRepository<User> userRepository;
        public IRepository<User> UserRepository => userRepository ?? new Repository<User>(context);

        private IRepository<Role> roleRepository;
        public IRepository<Role> RoleRepository => roleRepository ?? new Repository<Role>(context);

        private IRepository<UserRole> userRoleRepository;
        public IRepository<UserRole> UserRoleRepository => userRoleRepository ?? new Repository<UserRole>(context);

        private IRepository<Post> postRepository;
        public IRepository<Post> PostRepository => postRepository ?? new Repository<Post>(context);

        private IRepository<Comment> commentRepository;
        public IRepository<Comment> CommentRepository => commentRepository ?? new Repository<Comment>(context);

        private IRepository<Like> likeRepository;
        public IRepository<Like> LikeRepository => likeRepository ?? new Repository<Like>(context);

        private IRepository<Follower> followerRepository;
        public IRepository<Follower> FollowerRepository => followerRepository ?? new Repository<Follower>(context);

        private IRepository<Message> messageRepository;
        public IRepository<Message> MessageRepository => messageRepository ?? new Repository<Message>(context);

        private IRepository<Connection> connectionRepository;
        public IRepository<Connection> ConnectionRepository => connectionRepository ?? new Repository<Connection>(context);
        private IRepository<Story> storyRepository;
        public IRepository<Story> StoryRepository => storyRepository ?? new Repository<Story>(context);
        private IRepository<UserStory> userStoryRepository;
        public IRepository<UserStory> UserStoryRepository => userStoryRepository ?? new Repository<UserStory>(context);
        private IFileRepository fileRepository;
        public IFileRepository FileRepository => fileRepository ?? new FileRepository(context);

        #endregion

        public async Task<bool> Complete()
               => await context.SaveChangesAsync() > 0;

        public bool HasChanges()
            => context.ChangeTracker.HasChanges();

        public void Dispose()
        {
            context.Dispose();
        }
    }
}