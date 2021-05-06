using System.Threading.Tasks;
using MyStagram.Core.Data.Models;
using MyStagram.Core.Logic.Requests.Query.Messenger;
using MyStagram.Core.Models.Domain.Social;

namespace MyStagram.Core.Data.Repositories
{
    public interface IMessageRepository : IRepository<Message>
    {
        Task<IPagedList<Message>> GetMessages(GetMessagesThreadRequest request, string senderId);
    }
}