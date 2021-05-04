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
    public class StartConnectionCommand : IRequestHandler<StartConnectionRequest, StartConnectionResponse>
    {
        private readonly IConnectionManager connectionManager;
        public StartConnectionCommand(IConnectionManager connectionManager)
        {
            this.connectionManager = connectionManager;

        }
        public async Task<StartConnectionResponse> Handle(StartConnectionRequest request, CancellationToken cancellationToken)
        => await connectionManager.StartConnection(request.ConnectionId)
        ? new StartConnectionResponse()
        : new StartConnectionResponse(Error.Build
        (
            ErrorCodes.ConnectionFailed,
            "Start hub connection failed"
        ));
    }
}