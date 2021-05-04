using System.Threading.Tasks;

namespace MyStagram.Core.Services.SignalR
{
    public interface IConnectionManager
    {
        Task<bool> CloseConnection(string connectionId);
        Task<string> GetConnectionId(string userId);
        Task<bool> StartConnection(string connectionId);
    }
}