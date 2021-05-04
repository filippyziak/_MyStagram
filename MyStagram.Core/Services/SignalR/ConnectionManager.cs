using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MyStagram.Core.Extensions;
using MyStagram.Core.Models.Domain.Connection;
using MyStagram.Core.Data;

namespace MyStagram.Core.Services.SignalR
{
    public class ConnectionManager : IConnectionManager
    {
        private readonly IDatabase database;
        private readonly string currentUserId;
        public ConnectionManager(IDatabase database, IHttpContextAccessor httpContextAccessor)
        {
            this.database = database;
            this.currentUserId = httpContextAccessor.HttpContext.GetCurrentUserId();
        }

        public async Task<bool> StartConnection(string connectionId)
        {
            Connection connection = default(Connection);

            connection = await database.ConnectionRepository.Find(c => c.UserId == currentUserId);

            if (connection != null)
                database.ConnectionRepository.Delete(connection);

            connection = Connection.Create(currentUserId, connectionId);
            database.ConnectionRepository.Add(connection);

            return await database.Complete();
        }

        public async Task<bool> CloseConnection(string connectionId)
        {
            var connection = await database.ConnectionRepository.Find(c => c.ConnectionId == connectionId);

            if (connection != null)
            {
                database.ConnectionRepository.Delete(connection);
                return await database.Complete();
            }

            return true;
        }

        public async Task<string> GetConnectionId(string userId)
        => (await database.ConnectionRepository.Find(c => c.UserId == userId))?.ConnectionId;
    }
}