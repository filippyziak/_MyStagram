using MyStagram.Core.Models.Helpers.Error;

namespace MyStagram.Core.Logic.Responses.Command.Story
{
    public class DeleteStoryResponse : BaseResponse
    {
        public DeleteStoryResponse(Error error = null) : base(error) { }
    }
}