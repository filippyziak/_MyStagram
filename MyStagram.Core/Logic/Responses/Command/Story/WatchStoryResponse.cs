using MyStagram.Core.Models.Helpers.Error;

namespace MyStagram.Core.Logic.Responses.Command.Story
{
    public class WatchStoryResponse : BaseResponse
    {
        public WatchStoryResponse(Error error = null) : base(error) { }
    }
}