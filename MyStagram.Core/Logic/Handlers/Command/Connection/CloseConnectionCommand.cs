using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyStagram.Core.Helpers;
using MyStagram.Core.Logic.Requests.Command.Connection;
using MyStagram.Core.Logic.Responses.Command.Connection;
using MyStagram.Core.Models.Helpers.Error;
using MyStagram.Core.Services.SignalR;

namespace MyStagram.Core.Logic.Handlers.Command.Connection
{
    public class CloseConnectionCommand : IRequestHandler<CloseConnectionRequest, CloseConnectionResponse>
    {
        private readonly IConnectionManager connectionManager;
        public CloseConnectionCommand(IConnectionManager connectionManager)
        {
            this.connectionManager = connectionManager;

        }

        public async Task<CloseConnectionResponse> Handle(CloseConnectionRequest request, CancellationToken cancellationToken)
        => await connectionManager.CloseConnection(request.ConnectionId)
        ? new CloseConnectionResponse()
        : new CloseConnectionResponse(Error.Build
        (
            ErrorCodes.ConnectionFailed,
            "Close hub connection failed"
        ));
    }
}