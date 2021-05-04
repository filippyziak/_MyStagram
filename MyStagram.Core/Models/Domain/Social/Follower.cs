using System;
using MyStagram.Core.Models.Domain.Auth;

namespace MyStagram.Core.Models.Domain.Social
{
    public class Follower
    {
        public string SenderId { get; protected set; }
        public string RecipientId { get; protected set; }
        public bool RecipientAccepted { get; protected set; }
        public bool IsWatched { get; protected set; }
        public DateTime Created { get; protected set; } = DateTime.Now;

        public virtual User Sender { get; protected set; }
        public virtual User Recipient { get; protected set; }

        public void IsAccepted(bool recipientAccepted)
        {
            this.RecipientAccepted = recipientAccepted;
        }

        public void SentFrom(string senderId)
        {
            this.SenderId = senderId;
        }

        public void SentTo(string recipientId)
        {
            this.RecipientId = recipientId;
        }

        public void MarkAsWatched()
        {
            this.IsWatched = true;
        }
    }
}