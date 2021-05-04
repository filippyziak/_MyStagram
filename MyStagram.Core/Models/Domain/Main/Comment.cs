using System;
using MyStagram.Core.Helpers;
using MyStagram.Core.Models.Domain.Auth;

namespace MyStagram.Core.Models.Domain.Main
{
    public class Comment
    {
        public string Id { get; protected set; } = Utils.Id();
        public string Content { get; protected set; }
        public DateTime Created { get; protected set; } = DateTime.Now;
        public string UserId { get; protected set; }
        public string PostId { get; protected set; }

        public virtual User User { get; protected set; }
        public virtual Post Post { get; protected set; }

        public static Comment Create(string content) => new Comment { Content = content };

        public void SetContent(string content)
        {
            Content = content;
        }
    }
}