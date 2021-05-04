using MyStagram.Core.Models.Helpers.Error;

namespace MyStagram.Core.Logic.Responses.Query.Auth
{
    public class ConfirmAccountResponse : BaseResponse
    {
        public ConfirmAccountResponse(Error error = null) : base(error) { }
    }
}