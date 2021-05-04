using MediatR;
using MyStagram.Core.Logic.Responses.Command.Auth;

namespace MyStagram.Core.Logic.Requests.Command.Auth
{
    public class SendResetPasswordRequest : IRequest<SendResetPasswordResponse>
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}