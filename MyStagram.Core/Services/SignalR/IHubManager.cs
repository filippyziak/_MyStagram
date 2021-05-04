using System.Threading.Tasks;

namespace MyStagram.Core.Services.SignalR
{
    public interface IHubManager
    {
        Task Invoke(string actionName, string clientId, params object[] values);
        Task InvokeToAll(string actionName, params object[] values); 
    }
}