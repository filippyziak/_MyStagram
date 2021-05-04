using MyStagram.Core.Models.Domain.Social;

namespace MyStagram.Core.Builders.Interface
{
    public interface IFollowerBuilder : IBuilder<Follower>
    {
       IFollowerBuilder SentFrom(string senderId);
       IFollowerBuilder SentTo(string recipientId);  
       IFollowerBuilder IsAccepted(bool recipientAccepted);  
    }
}