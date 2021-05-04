using MyStagram.Core.Models.Helpers.Error;

namespace MyStagram.Core.Logic.Responses.Command.Admin
{
    public class RevokeRoleResponse : BaseResponse
    {
        public RevokeRoleResponse(Error error = null) : base(error) { }
    }
}