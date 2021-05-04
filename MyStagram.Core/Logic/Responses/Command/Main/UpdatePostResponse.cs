using MyStagram.Core.Models.Helpers.Error;
using MyStagram.Core.Models.Dtos.Main;

namespace MyStagram.Core.Logic.Responses.Command.Main
{
    public class UpdatePostResponse : BaseResponse
    {
        public PostDto Post { get; set; }
        public UpdatePostResponse(Error error = null) : base(error) { }
    }
}