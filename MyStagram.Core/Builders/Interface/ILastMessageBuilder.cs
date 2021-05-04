using System;
using MyStagram.Core.Models.Helpers.Messenger;

namespace MyStagram.Core.Builders.Interface
{
    public interface ILastMessageBuilder : IBuilder<LastMessage>
    {
         ILastMessageBuilder SetContent(string content);
         ILastMessageBuilder CreatedOn(DateTime dateCreated);
         ILastMessageBuilder MarkAsRead(bool isRead);
         ILastMessageBuilder SentBy(string senderId, string senderUserName, string senderPhotoUrl);
    }
}