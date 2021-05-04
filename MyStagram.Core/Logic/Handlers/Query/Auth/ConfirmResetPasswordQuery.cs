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
    public class ConfirmResetPasswordQuery : IRequestHandler<ConfirmResetPasswordRequest, ConfirmResetPasswordResponse>
    {
        private readonly IAuthService authService;
        public ConfirmResetPasswordQuery(IAuthService authService)
        {
            this.authService = authService;

        }
        public async Task<ConfirmResetPasswordResponse> Handle(ConfirmResetPasswordRequest request, CancellationToken cancellationToken)
        {
           var result = await authService.ConfirmResetPassword(request.UserId, request.Token);
           return result ? new ConfirmResetPasswordResponse() : new ConfirmResetPasswordResponse(Error.Build
           (
               ErrorCodes.TokenInvalid,
               "Token is invalid"
           ));
        } 
    }
}