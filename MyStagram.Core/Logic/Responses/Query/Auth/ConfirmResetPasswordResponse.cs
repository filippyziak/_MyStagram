using MyStagram.Core.Models.Helpers.Error;

namespace MyStagram.Core.Logic.Responses.Query.Auth
{
    public class ConfirmResetPasswordResponse : BaseResponse
    {
        public ConfirmResetPasswordResponse(Error error = null) : base(error) { }
    }
}