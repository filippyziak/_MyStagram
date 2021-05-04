using MyStagram.Core.Models.Helpers.Error;

namespace MyStagram.Core.Logic.Responses.Command.Admin
{
    public class DeleteUserResponse : BaseResponse
    {
        public DeleteUserResponse(Error error = null) : base(error) { }
    }
}