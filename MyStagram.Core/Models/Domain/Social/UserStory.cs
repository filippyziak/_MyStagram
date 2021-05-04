using MyStagram.Core.Models.Domain.Auth;

namespace MyStagram.Core.Models.Domain.Social
{
    public class UserStory
    {
        public string StoryId { get; protected set; }
        public string UserId { get; protected set; }

        public virtual Story Story { get; protected set; }
        public virtual User User { get; protected set; }

        public static UserStory Create(string storyId, string userId) => new UserStory
        {
            StoryId = storyId,
            UserId = userId
        };
    }
}