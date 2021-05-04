using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyStagram.Infrastructure.Database.Configs;
using MyStagram.Core.Models.Helpers.File;
using MyStagram.Core.Models.Domain.Auth;
using MyStagram.Core.Models.Domain.Connection;
using MyStagram.Core.Models.Domain.Main;
using MyStagram.Core.Models.Domain.Social;

namespace MyStagram.Infrastructure.Database
{
    public class DataContext : IdentityDbContext<User, Role, string, IdentityUserClaim<string>,
        UserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        #region tables

        #endregion

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Like> Likes { get; set; }
        public virtual DbSet<Follower> Followers { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Story> Stories { get; set; }
        public virtual DbSet<UserStory> UserStories { get; set; }
        public DbSet<Core.Models.Domain.File.File> Files { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            new UserConfig().Configure(builder.Entity<User>());
            new UserRoleConfig().Configure(builder.Entity<UserRole>());

            new PostConfig().Configure(builder.Entity<Post>());
            new LikeConfig().Configure(builder.Entity<Like>());

            new FollowerConfig().Configure(builder.Entity<Follower>());
            new MessageConfig().Configure(builder.Entity<Message>());
            new StoryConfig().Configure(builder.Entity<Story>());
            new UserStoryConfig().Configure(builder.Entity<UserStory>());

            new ConnectionConfig().Configure(builder.Entity<Connection>());

        }

    }
}