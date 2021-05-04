using MyStagram.Core.Models.Helpers.Error;

namespace MyStagram.Core.Logic.Responses.Command.Profile
{
    public class ChangePasswordResponse : BaseResponse
    {
        public ChangePasswordResponse(Error error = null) : base(error) { }
    }
}