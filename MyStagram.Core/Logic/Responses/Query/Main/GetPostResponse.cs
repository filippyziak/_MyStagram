using MyStagram.Core.Models.Helpers.Error;
using MyStagram.Core.Models.Dtos.Main;

namespace MyStagram.Core.Logic.Responses.Query.Main
{
    public class GetPostResponse : BaseResponse
    {
        public PostDto Post { get; set; }        
        public GetPostResponse(Error error = null) : base(error) { }
    }
}