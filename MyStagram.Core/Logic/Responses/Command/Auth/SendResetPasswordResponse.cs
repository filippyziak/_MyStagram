using MyStagram.Core.Models.Helpers.Error;

namespace MyStagram.Core.Logic.Responses.Command.Auth
{
    public class SendResetPasswordResponse : BaseResponse
    {
        public SendResetPasswordResponse(Error error = null) : base(error) { }
    }
}