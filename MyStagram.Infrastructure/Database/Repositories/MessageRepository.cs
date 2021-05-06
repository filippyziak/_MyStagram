using System.Linq;
using System.Threading.Tasks;
using MyStagram.Core.Data.Models;
using MyStagram.Core.Data.Repositories;
using MyStagram.Core.Extensions;
using MyStagram.Core.Logic.Requests.Query.Messenger;
using MyStagram.Core.Models.Domain.Social;

namespace MyStagram.Infrastructure.Database.Repositories
{
    public class MessageRepository : Repository<Message>, IMessageRepository
    {
        public MessageRepository(DataContext context) : base(context)
        {
        }

        public async Task<IPagedList<Message>> GetMessages(GetMessagesThreadRequest request, string senderId)
        => await context.Messages
            .Where(m => (m.SenderId == senderId && m.RecipientId == request.RecipientId)
            || (m.SenderId == request.RecipientId && m.RecipientId == senderId))
            .OrderByDescending(m => m.DateCreated)
            .ToPagedList<Message>(request.PageNumber, request.PageSize);
    }
}