using MyStagram.Core.Builders.Interface;
using MyStagram.Core.Models.Domain.Social;

namespace MyStagram.Core.Builders
{
    public class FollowerBuilder : IFollowerBuilder
    {
        private readonly Follower follower = new Follower();
        public Follower Build()
            =>follower;
        

        public IFollowerBuilder IsAccepted(bool recipientAccepted)
        {
            follower.IsAccepted(recipientAccepted);
            return this;
        }

        public IFollowerBuilder SentFrom(string senderId)
        {
            follower.SentFrom(senderId);
            return this;
        }

        public IFollowerBuilder SentTo(string recipientId)
        {
            follower.SentTo(recipientId);
            return this;
        }
    }
}