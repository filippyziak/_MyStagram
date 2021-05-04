using MyStagram.Core.Models.Helpers.Error;

namespace MyStagram.Core.Logic.Responses.Command.Admin
{
    public class BlockUserResponse : BaseResponse
    {
        public bool IsBlocked { get; set; }
        public BlockUserResponse(Error error = null) : base(error) { }
    }
}