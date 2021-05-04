using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyStagram.Core.Helpers;
using MyStagram.Core.Logic.Requests.Query.Auth;
using MyStagram.Core.Logic.Responses.Query.Auth;
using MyStagram.Core.Models.Helpers.Error;
using MyStagram.Core.Services.Interfaces;

namespace MyStagram.Core.Logic.Handlers.Query.Auth
{
    public class ConfirmAccountCommand : IRequestHandler<ConfirmAccountRequest, ConfirmAccountResponse>
    {
        private readonly IAuthService authService;
        public ConfirmAccountCommand(IAuthService authService)
        {
            this.authService = authService;

        }
        public async Task<ConfirmAccountResponse> Handle(ConfirmAccountRequest request, CancellationToken cancellationToken)
        => await authService.ConfirmAccount(request.UserId, request.Token)
        ? new ConfirmAccountResponse() : new ConfirmAccountResponse(Error.Build
        (
            ErrorCodes.AuthError,
                "Some error occurred during confirming account"
        ));
    }
}