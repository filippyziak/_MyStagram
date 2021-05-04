using MyStagram.Core.Models.Helpers.Error;
using MyStagram.Core.Models.Dtos.Main;

namespace MyStagram.Core.Logic.Responses.Command.Main
{
    public class CreateCommentResponse : BaseResponse
    {
        public CommentDto Comment { get; set; }
        public CreateCommentResponse(Error error = null) : base(error) { }
    }
}