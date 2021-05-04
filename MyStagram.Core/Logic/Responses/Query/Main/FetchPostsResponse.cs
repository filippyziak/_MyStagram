using System.Collections.Generic;
using MyStagram.Core.Models.Helpers.Error;
using MyStagram.Core.Models.Dtos.Main;

namespace MyStagram.Core.Logic.Responses.Query.Main
{
    public class FetchPostsResponse : BaseResponse
    {
        public List<PostDto> Posts { get; set; }
        public FetchPostsResponse(Error error = null) : base(error) { }
    }
}