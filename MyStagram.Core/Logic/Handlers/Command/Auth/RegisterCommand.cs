using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using MyStagram.Core.Helpers;
using MyStagram.Core.Logic.Requests.Command.Auth;
using MyStagram.Core.Logic.Responses.Command.Auth;
using MyStagram.Core.Models.Helpers.Error;
using MyStagram.Core.Services.Interfaces;

namespace MyStagram.Core.Logic.Handlers.Command.Auth
{
    public class RegisterCommand : IRequestHandler<RegisterRequest, RegisterResponse>
    {
        private readonly IAuthService authService;
        private readonly IMapper mapper;
        private readonly IEmailSender emailSender;

        public IConfiguration Configuration { get; }

        public RegisterCommand(IAuthService authService, IMapper mapper,
                                IConfiguration configuration, IEmailSender emailSender)
        {
            this.mapper = mapper;
            this.authService = authService;
            Configuration = configuration;
            this.emailSender = emailSender;
        }
        public async Task<RegisterResponse> Handle(RegisterRequest request, CancellationToken cancellationToken)
        {
            if (authService.EmailExists(request.Email).Result)
                return new RegisterResponse(Error.Build
                (
                    ErrorCodes.EmailExists,
                    "Email already exists"
                ));

            if (authService.UserNameExists(request.UserName).Result)
                return new RegisterResponse(Error.Build
                (
                    ErrorCodes.UsernameExists,
                    "Username already exists"
                ));

            var signUpResult = await authService.SignUp(request.Email, request.Password, request.UserName);
            if (signUpResult != null)
            {
            var confirmEmailUrl = await authService.GeneratedConfirmEmailUrl(signUpResult.User.Id);
        
                return await emailSender.Send(Constants.ActivationAccountEmail(request.Email, request.UserName,
                       confirmEmailUrl))
                       ? new RegisterResponse()
                       : new RegisterResponse(Error.Build
                       (
                           ErrorCodes.ServiceError,
                           "Some error occurred during sending an email message"
                       ));
            }

            return new RegisterResponse(Error.Build
            (
                ErrorCodes.AuthError,
                "Some error occrred during signing up"
            ));
        }
    }
}