using MyStagram.Core.Models.Helpers.Error;

namespace MyStagram.Core.Logic.Responses.Command.Auth
{
    public class ResetPasswordResponse : BaseResponse
    {
        public ResetPasswordResponse(Error error = null) : base(error) { }
    }
}