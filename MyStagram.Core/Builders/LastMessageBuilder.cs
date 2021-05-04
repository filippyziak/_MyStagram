using System;
using MyStagram.Core.Builders.Interface;
using MyStagram.Core.Models.Helpers.Messenger;

namespace MyStagram.Core.Builders
{
    public class LastMessageBuilder : ILastMessageBuilder
    {
        private readonly LastMessage lastMessage = new LastMessage();
        public LastMessage Build()
        => this.lastMessage;

        public ILastMessageBuilder CreatedOn(DateTime dateCreated)
        {
           this.lastMessage.DateCreated = dateCreated;
           return this;
        }

        public ILastMessageBuilder MarkAsRead(bool isRead)
        {
            this.lastMessage.IsRead = isRead;
            return this;
        }

        public ILastMessageBuilder SentBy(string senderId, string senderUserName, string senderPhotoUrl)
        {
            this.lastMessage.SenderId = senderId;
            this.lastMessage.SenderUserName = senderUserName;
            this.lastMessage.SenderPhotoUrl = senderPhotoUrl;
            return this;
        }

        public ILastMessageBuilder SetContent(string content)
        {
            this.lastMessage.Content = content;
            return this;
        }
    }
}