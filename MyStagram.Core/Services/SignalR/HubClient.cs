using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace MyStagram.Core.Services.SignalR
{
    public class HubClient : Hub
    {
        private readonly IConnectionManager connectionManager;
        public HubClient(IConnectionManager connectionManager)
        {
            this.connectionManager = connectionManager;
        }

        public string GetConnectionId() => Context.ConnectionId;

        public async override Task OnDisconnectedAsync(Exception exception)
        {
            await connectionManager.CloseConnection(Context.ConnectionId);

            await base.OnDisconnectedAsync(exception);
        }
    }
}