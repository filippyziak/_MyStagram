using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyStagram.Core.Enums;
using MyStagram.Core.Logic.Requests.Query.Auth;
using MyStagram.Core.Logic.Responses.Query.Auth;
using MyStagram.Core.Services.Interfaces.ReadOnly;

namespace MyStagram.Core.Logic.Handlers.Query.Auth
{
    public class GetRegisterValidationQuery : IRequestHandler<GetRegisterValidationRequest, GetRegisterValidationResponse>
    {
        private readonly IReadOnlyAuthService authService;

        public GetRegisterValidationQuery(IReadOnlyAuthService authService)
        {
            this.authService = authService;

        }
        public async Task<GetRegisterValidationResponse> Handle(GetRegisterValidationRequest request, CancellationToken cancellationToken)
        {
            bool isAvailable = false;

            switch (request.RegisterValidation)
            {
                case RegisterValidation.Email:
                    isAvailable = !(await authService.EmailExists(request.Content));
                    break;
                case RegisterValidation.UserName:
                    isAvailable = !(await authService.UserNameExists(request.Content));
                    break;
                default: break;
            }

            return new GetRegisterValidationResponse { IsAvailable = isAvailable };

        }
    }
}