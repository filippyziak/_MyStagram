using MediatR;
using MyStagram.Core.Logic.Responses.Command.Auth;

namespace MyStagram.Core.Logic.Requests.Command.Auth
{
    public class LoginRequest : IRequest<LoginResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}