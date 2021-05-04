using MyStagram.Core.Models.Helpers.Error;

namespace MyStagram.Core.Logic.Responses.Command.Auth
{
    public class RegisterResponse : BaseResponse
    {
        public RegisterResponse(Error error = null) : base(error) { }
    }
}