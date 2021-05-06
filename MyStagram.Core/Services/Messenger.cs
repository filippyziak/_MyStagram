using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyStagram.Core.Exceptions;
using MyStagram.Core.Extensions;
using MyStagram.Core.Logic.Requests.Query.Messenger;
using MyStagram.Core.Models.Helpers.Pagination;
using MyStagram.Core.Services.Interfaces;
using MyStagram.Core.Models.Domain.Social;
using MyStagram.Core.Models.Helpers.Messenger;
using MyStagram.Core.Builders;
using MyStagram.Core.Data;
using MyStagram.Core.Services.Interfaces.ReadOnly;
using MyStagram.Core.Data.Models;

namespace MyStagram.Core.Services
{
    public class Messenger : IMessenger
    {
        private readonly IDatabase database;
        private readonly IReadOnlyProfileService profileService;
        private readonly IReadOnlyFollowersService followersService;
        public Messenger(IDatabase database, IReadOnlyProfileService profileService, IReadOnlyFollowersService followersService)
        {
            this.database = database;
            this.profileService = profileService;
            this.followersService = followersService;
        }

        public async Task<Message> Send(string recipientId, string content)
        {
            var sender = await this.profileService.GetCurrentUser();
            var recipient = await this.profileService.GetUser(recipientId);

            if (sender.Id == recipientId)
                throw new NoPermissionsException("You cannot send message to yourself");

            if (recipient.IsPrivate)
            {
                var follower = await this.followersService.GetFollower(sender.Id, recipientId);

                if (sender.MessagesReceived.Any(m => m.SenderId == recipientId)
                    || follower != null && follower.RecipientAccepted)
                {
                    var message = CreateMessage(sender.Id, recipientId, content);

                    return await database.Complete() ? message : null;
                }
                else
                    return null;
            }
            else
            {
                var message = CreateMessage(sender.Id, recipientId, content);

                return await database.Complete() ? message : null;
            }
        }

        public async Task<IPagedList<Message>> GetMessagesThread(GetMessagesThreadRequest request)
        {

            var sender = await this.profileService.GetCurrentUser();

            if (sender.Id == request.RecipientId)
                throw new EntityNotFoundException("Messages thread not found");

            var messages = await database.MessageRepository.GetMessages(request, sender.Id);

            messages = await MarkAsRead(sender.Id, request.RecipientId, messages);

            return messages;

        }

        public async Task<PagedList<Conversation>> GetConverstaions(GetConversationsRequest request)
        {
            var sender = await this.profileService.GetCurrentUser();

            var conversations = sender.MessagesSent.Concat(sender.MessagesReceived)
            .Where(m => string.IsNullOrEmpty(request.UserName)
            ? true
            : (m.SenderId == sender.Id ? m.Recipient.UserName.ToLower().Contains(request.UserName.ToLower())
            : m.Sender.UserName.ToLower().Contains(request.UserName.ToLower())))
            .OrderByDescending(m => m.DateCreated)
            .GroupBy(m => new { m.SenderId, m.RecipientId })
            .Select(g =>
             {
                 var message = g.First();
                 var lastMessage = new LastMessageBuilder()
                                 .SetContent(message.Content)
                                 .CreatedOn(message.DateCreated)
                                 .MarkAsRead(message.IsRead)
                                 .SentBy(message.SenderId, message.Sender.UserName, message.Sender.PhotoUrl)
                                 .Build();
                 var conversation = new ConversationBuilder()
                 .SentBy(message.SenderId)
                 .SentTo(message.RecipientId)
                 .SetLastMessage(lastMessage)
                 .SetUserData(message.SenderId == sender.Id ? message.Recipient.UserName : message.Sender.UserName,
                 message.SenderId == sender.Id ? message.Recipient.PhotoUrl : message.Sender.PhotoUrl)
                 .Build();

                 return conversation;
             })
             .ToList();

            var uniqueConversations = new List<Conversation>();

            conversations.ForEach(c =>
           {
               if (uniqueConversations.Count == 0 || !uniqueConversations.Any(uc => (uc.SenderId == c.SenderId && uc.RecipientId == c.RecipientId)
                 || (uc.SenderId == c.RecipientId && uc.RecipientId == c.SenderId)))
                   uniqueConversations.Add(c);
           });

            return PagedList<Conversation>.Create(uniqueConversations, request.PageNumber, request.PageSize);
        }

        public async Task<int> CountUnreadConversations()
        {
            var user = await this.profileService.GetCurrentUser();
            return user.MessagesReceived.OrderByDescending(m => m.DateCreated)
            .GroupBy(m => new { m.SenderId })
            .Where(g => !g.First().IsRead)
            .Count();
        }

        public async Task<int> CountUnreadMessages(string senderId)
        {
            var user = await this.profileService.GetCurrentUser();
            return user.MessagesReceived.Where(m => m.SenderId == senderId).OrderByDescending(m => m.DateCreated)
            .TakeWhile(m => !m.IsRead)
            .Count();
        }

        public async Task<bool> ReadMessage(string messageId)
        {
            var message = await database.MessageRepository.Get(messageId);
            message.ReadMessage();
            database.MessageRepository.Update(message);
            return await database.Complete();
        }
        private Message CreateMessage(string senderId, string recipientId, string content)
        {
            var message = Message.Create(senderId, recipientId, content);
            database.MessageRepository.Add(message);
            return message;
        }

        private async Task<IPagedList<Message>> MarkAsRead(string currentUserId, string recipientId, IPagedList<Message> userMessages)
        {
            if ((userMessages.FirstOrDefault())?.RecipientId != currentUserId)
                return userMessages;

            userMessages.TakeWhile(m => !m.IsRead).ToList().ForEach(m => m.ReadMessage());

            await database.Complete();

            return userMessages;
        }
    }
}