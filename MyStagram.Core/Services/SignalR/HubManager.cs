using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace MyStagram.Core.Services.SignalR
{


    public class HubManager : IHubManager
    {
        private readonly IHubContext<HubClient> hubContext;
        private readonly IConnectionManager connectionManger;
        public HubManager(IHubContext<HubClient> hubContext, IConnectionManager connectionManger)
        {
            this.hubContext = hubContext;
            this.connectionManger = connectionManger;
        }

        public async Task Invoke(string actionName, string clientId, params object[] values)
        {
            string connectionId = await connectionManger.GetConnectionId(clientId);

            if (!string.IsNullOrEmpty(connectionId))
                await hubContext.Clients.Client(await connectionManger.GetConnectionId(clientId)).SendAsync(actionName, values);
        }

        public async Task InvokeToAll(string actionName, params object[] values)
            => await hubContext.Clients.All.SendAsync(actionName, values);

    }
}