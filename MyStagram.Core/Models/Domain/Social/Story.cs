
using System;
using System.Collections.Generic;
using MyStagram.Core.Helpers;
using MyStagram.Core.Models.Domain.Auth;

namespace MyStagram.Core.Models.Domain.Social
{
    public class Story
    {
        public string Id { get; protected set; } = Utils.Id();
        public DateTime DateCreated { get; protected set; } = DateTime.Now;
        public DateTime DateExpires { get; protected set; } = DateTime.Now.AddDays(1);
        public string StoryUrl { get; protected set; }
        public string UserId { get; protected set; }

        public virtual User User { get; protected set; }
        
        public virtual ICollection<UserStory> UserStories { get; protected set; } = new HashSet<UserStory>();

        public void SetStoryUrl(string storyUrl)
        {
            this.StoryUrl = storyUrl;
        }
    }
}