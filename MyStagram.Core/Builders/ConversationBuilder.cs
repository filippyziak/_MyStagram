using MyStagram.Core.Builders.Interface;
using MyStagram.Core.Models.Helpers.Messenger;

namespace MyStagram.Core.Builders
{
    public class ConversationBuilder : IConversationBuilder
    {
        private readonly Conversation conversation = new Conversation();

        public Conversation Build()
        => this.conversation;

        public IConversationBuilder SentBy(string senderId)
        {
            this.conversation.SenderId = senderId;
            return this;
        }

        public IConversationBuilder SentTo(string recipientId)
        {
            this.conversation.RecipientId = recipientId;
            return this;
        }

        public IConversationBuilder SetLastMessage(LastMessage lastMessage)
        {
            this.conversation.LastMessage = lastMessage;
            return this;
        }

        public IConversationBuilder SetUserData(string userName, string avatarUrl)
        {
            this.conversation.UserName = userName;
            this.conversation.AvatarUrl = avatarUrl;
            return this;
        }
    }
}