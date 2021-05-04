using MyStagram.Core.Models.Helpers.Error;
using MyStagram.Core.Models.Dtos.Social;

namespace MyStagram.Core.Logic.Responses.Command.Social
{
    public class FollowUserResponse : BaseResponse
    {
        public FollowerDto Follower { get; set; }
        
        public FollowUserResponse(Error error = null) : base(error) { }
    }
}