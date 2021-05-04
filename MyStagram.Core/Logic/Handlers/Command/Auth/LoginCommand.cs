using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MyStagram.Core.Helpers;
using MyStagram.Core.Logic.Requests.Command.Auth;
using MyStagram.Core.Logic.Responses.Command.Auth;
using MyStagram.Core.Models.Helpers.Error;
using MyStagram.Core.Services.Interfaces;
using MyStagram.Core.Models.Dtos.Auth;

namespace MyStagram.Core.Logic.Handlers.Command.Auth
{
    public class LoginCommand : IRequestHandler<LoginRequest, LoginResponse>
    {
        private readonly IMapper mapper;
        private readonly IAuthService authService;
        public LoginCommand(IMapper mapper, IAuthService authService)
        {
            this.authService = authService;
            this.mapper = mapper;

        }
        public async Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var signInResult = await this.authService.SignIn(request.Email, request.Password);

            if (signInResult != null)
                return new LoginResponse
                {
                    Token = signInResult.Token,
                    User = mapper.Map<UserAuthDto>(signInResult.User)
                };
            
            return new LoginResponse(Error.Build(
                ErrorCodes.AuthError,
                "Some error occured during signing in"
            ));
        }
    }
}