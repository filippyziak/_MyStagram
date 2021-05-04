using System.Collections.Generic;
using MyStagram.Core.Models.Helpers.Error;
using MyStagram.Core.Models.Dtos.Main;

namespace MyStagram.Core.Logic.Responses.Query.Main
{
    public class GetPostsResponse : BaseResponse
    {
        public List<PostsDto> Posts { get; set; }
        public GetPostsResponse(Error error = null) : base(error) { }
    }
}