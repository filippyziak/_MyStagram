using System;
using System.Collections.Generic;
using MyStagram.Core.Helpers;
using MyStagram.Core.Models.Domain.Auth;

namespace MyStagram.Core.Models.Domain.Main
{
    public class Post
    {
        public string Id { get; protected set; } = Utils.Id();
        public string Description { get; protected set; }
        public DateTime Created { get; protected set; } = DateTime.Now;
        public string PhotoUrl { get; protected set; }
        public string UserId { get; protected set; }
        

        public virtual User User { get; protected set; }

        public virtual ICollection<Comment> Comments { get; protected set; } = new HashSet<Comment>();
        public virtual ICollection<Like> Likes { get; protected set; } = new HashSet<Like>();

        public void SetPhoto(string photoUrl)
        {
            PhotoUrl = photoUrl;
        }
    }
}