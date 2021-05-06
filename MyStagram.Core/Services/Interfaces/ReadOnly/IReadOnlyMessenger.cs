using System.Threading.Tasks;
using MyStagram.Core.Data.Models;
using MyStagram.Core.Logic.Requests.Query.Messenger;
using MyStagram.Core.Models.Domain.Social;
using MyStagram.Core.Models.Helpers.Messenger;
using MyStagram.Core.Models.Helpers.Pagination;

namespace MyStagram.Core.Services.Interfaces.ReadOnly
{
    public interface IReadOnlyMessenger
    {
        Task<IPagedList<Message>> GetMessagesThread(GetMessagesThreadRequest request);
        Task<PagedList<Conversation>> GetConverstaions(GetConversationsRequest request);
        Task<int> CountUnreadConversations();
        Task<int> CountUnreadMessages(string senderId);
    }
}