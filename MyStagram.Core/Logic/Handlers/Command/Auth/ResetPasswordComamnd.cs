using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyStagram.Core.Helpers;
using MyStagram.Core.Logic.Requests.Command.Auth;
using MyStagram.Core.Logic.Responses.Command.Auth;
using MyStagram.Core.Models.Helpers.Error;
using MyStagram.Core.Services.Interfaces;

namespace MyStagram.Core.Logic.Handlers.Command.Auth
{
    public class ResetPasswordComamnd : IRequestHandler<ResetPasswordRequest, ResetPasswordResponse>
    {
        private readonly IAuthService authService;
        public ResetPasswordComamnd(IAuthService authService)
        {
            this.authService = authService;

        }
        public async Task<ResetPasswordResponse> Handle(ResetPasswordRequest request, CancellationToken cancellationToken)
        {
            return await authService.ResetPassword(request.UserId, request.NewPassword, request.Token)
            ? new ResetPasswordResponse()
            : new ResetPasswordResponse(Error.Build
            (
                ErrorCodes.ResetPasswordFailed,
                "Error occured during setting new password"
            ));
        }
    }
}