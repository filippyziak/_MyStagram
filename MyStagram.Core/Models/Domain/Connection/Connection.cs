using System;
using MyStagram.Core.Models.Domain.Auth;

namespace MyStagram.Core.Models.Domain.Connection
{
    public class Connection
    {
        public string UserId { get; protected set; }
        public string ConnectionId { get; protected set; }
        public DateTime DateEstablished { get; protected set; } = DateTime.Now;
        
        public virtual User User { get; protected set; }

        public static Connection Create(string userId, string connectionId) => new Connection { UserId = userId, ConnectionId = connectionId};

        public void SetConnectionId(string connectionId) 
        {
            ConnectionId = connectionId;
        }            
        
    }
}