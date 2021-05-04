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
    public class SendResetPasswordCommand : IRequestHandler<SendResetPasswordRequest, SendResetPasswordResponse>
    {
        private readonly IAuthService authService;
        private readonly IEmailSender emailSender;

        public SendResetPasswordCommand(IAuthService authService, IEmailSender emailSender)
        {
            this.emailSender = emailSender;
            this.authService = authService;

        }
        public async Task<SendResetPasswordResponse> Handle(SendResetPasswordRequest request, CancellationToken cancellationToken)
        {
            var resetPasswordUrl = await authService.GenerateResetPasswordUrl(request.Email);

            return await emailSender.Send(Constants.ResetPasswordEmail(request.Email, request.UserName,
                      resetPasswordUrl))
                      ? new SendResetPasswordResponse()
                      : new SendResetPasswordResponse(Error.Build
                      (
                          ErrorCodes.ServiceError,
                          "Some error occurred during sending an email message"
                      ));

        }
    }
}