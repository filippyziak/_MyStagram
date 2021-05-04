using MyStagram.Core.Models.Helpers.Error;
using MyStagram.Core.Models.Dtos.Auth;

namespace MyStagram.Core.Logic.Responses.Command.Auth
{
    public class LoginResponse : BaseResponse
    {
        public string Token { get; set; }
        public UserAuthDto User { get; set; }

        public LoginResponse(Error error = null) : base(error) { }
    }
}