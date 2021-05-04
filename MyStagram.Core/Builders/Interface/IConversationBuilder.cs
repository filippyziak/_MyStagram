using MyStagram.Core.Models.Helpers.Messenger;

namespace MyStagram.Core.Builders.Interface
{
    public interface IConversationBuilder : IBuilder<Conversation>
    {
         IConversationBuilder SentBy(string senderId);
         IConversationBuilder SentTo(string recipientId);
         IConversationBuilder SetLastMessage(LastMessage lastMessage);
         IConversationBuilder SetUserData(string userName, string avatarUrl);
    }
}