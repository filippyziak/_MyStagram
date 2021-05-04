using MyStagram.Core.Models.Helpers.Error;
using MyStagram.Core.Models.Dtos.Main;

namespace MyStagram.Core.Logic.Responses.Command.Main
{
    public class CreatePostResponse : BaseResponse
    {
        public PostDto Post { get; set; }
        public CreatePostResponse(Error error = null) : base(error) { }
    }
}