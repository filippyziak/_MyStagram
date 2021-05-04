using MyStagram.Core.Models.Domain.Auth;

namespace MyStagram.Core.Models.Domain.Main
{
    public class Like
    {
        public string UserId { get; set; }
        public string PostId { get; set; }

        public virtual User User { get; set; }
        public virtual Post Post { get; set; }

        public static Like Create(string userId, string postId) => new Like { UserId = userId, PostId = postId };
    }
}