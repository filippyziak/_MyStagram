using System.Threading.Tasks;
using MyStagram.Core.Models.Domain.Social;
using MyStagram.Core.Services.Interfaces.ReadOnly;

namespace MyStagram.Core.Services.Interfaces
{
    public interface IMessenger : IReadOnlyMessenger
    {
        Task<Message> Send(string recipientId, string content);
        Task<bool> ReadMessage(string messageId);
    }
}