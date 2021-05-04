using System.Linq;
using System.Collections.Generic;
using MyStagram.Core.Models.Dtos.Story;

namespace MyStagram.Core.Models.Helpers.Story
{
    public class StoryWrapper
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserPhotoUrl { get; set; }
        public bool IsWatched { get; private set; }
        public StoryDto StoryToWatch { get; set; }


        public List<StoryDto> Stories { get; }

        public StoryWrapper(string userId, string userName, string userPhotoUrl, List<StoryDto> stories, StoryDto story)
        {
            UserId = userId;
            UserName = userName;
            UserPhotoUrl = userPhotoUrl;
            StoryToWatch = story;

            Stories = new List<StoryDto>(stories);
        }

        public void SetIsWatched(string currentUserId)
        {
            IsWatched = Stories.All(s => s.IsWatched);
        }
    }
}