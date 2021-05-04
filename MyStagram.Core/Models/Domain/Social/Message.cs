using System;
using MyStagram.Core.Helpers;
using MyStagram.Core.Models.Domain.Auth;

namespace MyStagram.Core.Models.Domain.Social
{
    public class Message
    {
        public string Id { get; protected set; } = Utils.Id();
        public string SenderId { get; protected set; }
        public string RecipientId { get; protected set; }
        public string Content { get; protected set; }
        public DateTime DateCreated { get; protected set; } = DateTime.Now;
        public bool IsRead { get; protected set; }

        public virtual User Sender { get; protected set; }
        public virtual User Recipient { get; protected set; }


        public static Message Create(string senderId, string recipientId, string content) => new Message { SenderId = senderId, RecipientId = recipientId, Content = content };

        public void ReadMessage()
        {
            IsRead = true;
        }
    }
}