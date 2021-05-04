using MyStagram.Core.Models.Helpers.Error;

namespace MyStagram.Core.Logic.Responses.Command.Admin
{
    public class AdmitRoleResponse : BaseResponse
    {
        public AdmitRoleResponse(Error error = null) : base(error) { }
    }
}