using MyStagram.Core.Models.Helpers.Error;

namespace MyStagram.Core.Logic.Responses.Command.Social
{
    public class UnFollowUserResponse : BaseResponse
    {
        public UnFollowUserResponse(Error error = null) : base(error) { }
    }
}