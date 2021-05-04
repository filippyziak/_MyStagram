using System.Collections.Generic;
using MyStagram.Core.Models.Helpers.Error;
using MyStagram.Core.Models.Dtos.Social;

namespace MyStagram.Core.Logic.Responses.Query.Social
{
    public class GetFollowersResponse : BaseResponse
    {
        public List<FollowerDto> Followers { get; set; }
        public List<FollowerDto> Following { get; set; }

        public GetFollowersResponse(Error error = null) : base(error) { }
    }
}