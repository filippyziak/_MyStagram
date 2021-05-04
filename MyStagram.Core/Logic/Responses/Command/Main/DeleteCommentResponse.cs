using MyStagram.Core.Models.Helpers.Error;

namespace MyStagram.Core.Logic.Responses.Command.Main
{
    public class DeleteCommentResponse : BaseResponse
    {
        public DeleteCommentResponse(Error error = null) : base(error) { }
    }
}